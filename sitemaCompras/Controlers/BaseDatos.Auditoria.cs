using System;
using System.Data;
using System.Web;
using MySql.Data.MySqlClient;

namespace sitemaCompras.Controlers
{
    /// <summary>
    /// Módulo de Logs de Auditoría (versión liviana).
    /// Registra QUIÉN hace QUÉ, CUÁNDO y DESDE DÓNDE sobre solicitudes y sesiones.
    ///
    /// Principio inviolable: la auditoría NUNCA debe tumbar la operación de negocio.
    /// Por eso todo va envuelto en try/catch que jamás propaga, y la escritura usa
    /// su propia conexión cuando no hay transacción activa (para no interferir con
    /// el Cmd/Cn compartido de la operación en curso).
    /// </summary>
    public partial class BaseDatos
    {
        /// <summary>
        /// Escribe un registro de auditoría. Resuelve automáticamente usuario, rol
        /// e IP desde HttpContext.Current (Session + Request) cuando está disponible.
        /// Si no hay contexto web (ej. un job en background), se pueden pasar los
        /// datos del usuario por parámetro con <paramref name="idUsuarioOverride"/>
        /// y <paramref name="nombreUsuarioOverride"/>.
        /// </summary>
        public void RegistrarAuditoria(
            string accionClave,
            string entidad,
            int? idEntidad,
            int? etapaOrigen,
            int? etapaDestino,
            int? estatusOrigen,
            int? estatusDestino,
            string detalle,
            string resultado = "OK",
            string idUsuarioOverride = null,
            string nombreUsuarioOverride = null)
        {
            try
            {
                string idUsuario = idUsuarioOverride;
                string nombreUsuario = nombreUsuarioOverride;
                int? idTipo = null;
                int? idRolesF = null;
                string ip = null;

                // Contexto web: IP desde Request, identidad desde Session.
                HttpContext ctx = HttpContext.Current;
                if (ctx != null)
                {
                    try { if (ctx.Request != null) ip = ctx.Request.UserHostAddress; }
                    catch { /* Request no disponible en este punto del ciclo de vida */ }

                    if (ctx.Session != null)
                    {
                        if (string.IsNullOrEmpty(idUsuario) && ctx.Session["id"] != null)
                            idUsuario = ctx.Session["id"].ToString();
                        if (string.IsNullOrEmpty(nombreUsuario) && ctx.Session["username"] != null)
                            nombreUsuario = ctx.Session["username"].ToString();

                        int parsed;
                        if (ctx.Session["type"] != null && int.TryParse(ctx.Session["type"].ToString(), out parsed))
                            idTipo = parsed;
                        if (ctx.Session["rolesF"] != null && int.TryParse(ctx.Session["rolesF"].ToString(), out parsed))
                            idRolesF = parsed;
                    }
                }

                using (MySqlCommand auditCmd = new MySqlCommand("insertarAuditoria"))
                {
                    auditCmd.CommandType = CommandType.StoredProcedure;
                    auditCmd.CommandTimeout = 60;
                    auditCmd.Parameters.Add(new MySqlParameter("@p_idUsuario", (object)idUsuario ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_nombreUsuario", (object)nombreUsuario ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_idTipo", idTipo.HasValue ? (object)idTipo.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_idRolesF", idRolesF.HasValue ? (object)idRolesF.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_accionClave", (object)accionClave ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_entidad", (object)entidad ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_idEntidad", idEntidad.HasValue ? (object)idEntidad.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_etapaOrigen", etapaOrigen.HasValue ? (object)etapaOrigen.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_etapaDestino", etapaDestino.HasValue ? (object)etapaDestino.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_estatusOrigen", estatusOrigen.HasValue ? (object)estatusOrigen.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_estatusDestino", estatusDestino.HasValue ? (object)estatusDestino.Value : DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_detalle", (object)Truncar(detalle, 500) ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_ip", (object)ip ?? DBNull.Value));
                    auditCmd.Parameters.Add(new MySqlParameter("@p_resultado", (object)(string.IsNullOrEmpty(resultado) ? "OK" : resultado)));

                    if (_currentTransaction != null)
                    {
                        // Misma transacción que la operación de negocio: si esta hace
                        // rollback, el log también se revierte (consistencia).
                        auditCmd.Connection = Cn;
                        auditCmd.Transaction = _currentTransaction;
                        auditCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Conexión propia y aislada: no toca el Cn/Cmd compartido.
                        using (MySqlConnection auditCn = new MySqlConnection(Cn.ConnectionString))
                        {
                            auditCmd.Connection = auditCn;
                            auditCn.Open();
                            auditCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // La auditoría es best-effort: si falla, se registra el error y se
                // continúa. Jamás se propaga hacia la operación de negocio.
                ErrorHandler.LogError(ex, "RegistrarAuditoria", "audit accion=" + accionClave);
            }
        }

        /// <summary>
        /// Lee la etapa y el estatus ACTUALES de una solicitud usando una conexión
        /// propia (no el Cmd compartido). Se usa para capturar el "origen" de una
        /// transición antes de que la operación de negocio lo pise.
        /// Best-effort: si algo falla, devuelve null en ambos out.
        /// </summary>
        public void LeerEtapaEstatusSolicitud(int idSolicitud, out int? etapa, out int? estatus)
        {
            etapa = null;
            estatus = null;
            try
            {
                using (MySqlConnection cn = new MySqlConnection(Cn.ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand("datosSolicitudes", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.Add(new MySqlParameter("@id", idSolicitud));
                    cn.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            int idxEtapa = SafeOrdinal(rd, "idEtapas");
                            int idxEstatus = SafeOrdinal(rd, "idEstatus");
                            if (idxEtapa >= 0 && !rd.IsDBNull(idxEtapa))
                                etapa = Convert.ToInt32(rd.GetValue(idxEtapa));
                            if (idxEstatus >= 0 && !rd.IsDBNull(idxEstatus))
                                estatus = Convert.ToInt32(rd.GetValue(idxEstatus));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LeerEtapaEstatusSolicitud", "audit");
            }
        }

        /// <summary>
        /// Consulta la bitácora de auditoría con filtros opcionales.
        /// Convención de filtros idéntica a filtroSolicitudes:
        /// cadenas vacías / NULL numéricos / NULL fechas = sin filtro.
        /// </summary>
        public DataSet filtroAuditoria(string idUsuario, int? idEntidad, string accionClave, DateTime? desde, DateTime? hasta)
        {
            DataSet dsAudit = new DataSet();
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "filtroAuditoria";
                Cmd.Parameters.Add(new MySqlParameter("@p_idUsuario", (object)(idUsuario ?? "") ));
                Cmd.Parameters.Add(new MySqlParameter("@p_idEntidad", idEntidad.HasValue ? (object)idEntidad.Value : DBNull.Value));
                Cmd.Parameters.Add(new MySqlParameter("@p_accionClave", (object)(accionClave ?? "") ));
                Cmd.Parameters.Add(new MySqlParameter("@p_desde", desde.HasValue ? (object)desde.Value : DBNull.Value));
                Cmd.Parameters.Add(new MySqlParameter("@p_hasta", hasta.HasValue ? (object)hasta.Value : DBNull.Value));
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(dsAudit);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "filtroAuditoria", "DB operation failed");
            }
            return dsAudit;
        }

        /// <summary>
        /// Catálogo de acciones para el dropdown del panel administrativo.
        /// </summary>
        public DataSet listaAccionesAuditoria()
        {
            DataSet dsAcc = new DataSet();
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaAccionesAuditoria";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(dsAcc);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "listaAccionesAuditoria", "DB operation failed");
            }
            return dsAcc;
        }

        private static int SafeOrdinal(MySqlDataReader rd, string columna)
        {
            try { return rd.GetOrdinal(columna); }
            catch { return -1; }
        }

        private static string Truncar(string valor, int max)
        {
            if (string.IsNullOrEmpty(valor)) return valor;
            return valor.Length <= max ? valor : valor.Substring(0, max);
        }
    }
}
