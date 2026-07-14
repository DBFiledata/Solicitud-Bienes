using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace sitemaCompras.Controlers
{
    public partial class BaseDatos
    {
        public void ObtenerPasoPrevio(int idSolicitud, int userRoleF, bool isJefe, out int etapaPrevio, out int estatusPrevio)
        {
            // Valores por defecto (fallback seguro: Unidad, En espera de Aprobación)
            etapaPrevio = 1;
            estatusPrevio = 2;

            try
            {
                string[] datos = datosSolicitudes(idSolicitud);
                if (datos == null) return;

                int etapaActual = !string.IsNullOrEmpty(datos[7]) ? Convert.ToInt32(datos[7]) : 1;
                bool tieneCotizador = !string.IsNullOrEmpty(datos[13]);
                bool tieneOrdenCompra = !string.IsNullOrEmpty(datos[12]);

                bool tieneReserva = false;
                bool haPasadoPorPresidencia = false;
                bool haPasadoComprasDespuesDePresidencia = false;

                DataSet dsComents = listaComentariosSolicitudes(idSolicitud);
                if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsComents.Tables[0];
                    int indexPresidencia = -1;
                    int indexComprasFase3 = -1;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        if (row["idEstatus"] != DBNull.Value && Convert.ToInt32(row["idEstatus"]) == 17)
                        {
                            tieneReserva = true;
                        }
                        if (row["comentario"] != DBNull.Value && row["comentario"].ToString().ToUpper().Contains("SEPARA PARTIDA"))
                        {
                            tieneReserva = true;
                        }

                        if (row["descripciones"] != DBNull.Value)
                        {
                            string etapaDesc = row["descripciones"].ToString();
                            if (etapaDesc == "Presidencia")
                            {
                                haPasadoPorPresidencia = true;
                                if (indexPresidencia == -1) indexPresidencia = i;
                            }
                            if (etapaDesc == "Compras")
                            {
                                if (indexComprasFase3 == -1) indexComprasFase3 = i;
                            }
                        }
                    }

                    if (haPasadoPorPresidencia && indexPresidencia != -1 && indexComprasFase3 != -1 && indexComprasFase3 < indexPresidencia)
                    {
                        haPasadoComprasDespuesDePresidencia = true;
                    }
                }

                // Determinación del paso y cálculo del paso previo
                if (userRoleF == 1) // Paso 2: Aprobador Interno
                {
                    // Regresa al Paso 1 (Solicitante) para subsanar
                    etapaPrevio = 1; 
                    estatusPrevio = 6; // Subsanar
                }
                else if (userRoleF == 2) // DGAF Vo. Bo. (Paso 3, Paso 7b, Paso 14)
                {
                    if (tieneReserva && tieneOrdenCompra) // Paso 14
                    {
                        // Regresa al Paso 13: Jefe de Compras (segunda vuelta)
                        etapaPrevio = 9; // Compras
                        estatusPrevio = 4; // Enviado
                    }
                    else if (tieneCotizador && !tieneReserva) // Paso 7b
                    {
                        // Regresa al Paso 7: Jefe de Compras (Vo. Bo. segunda vuelta)
                        etapaPrevio = 9; // Compras
                        estatusPrevio = 4; // Enviado
                    }
                    else // Paso 3
                    {
                        // Regresa al Paso 2: Aprobador Interno (Unidad)
                        etapaPrevio = 1; // Unidad
                        estatusPrevio = 2; // En espera de Aprobación
                    }
                }
                else if (userRoleF == 9) // Jefe de Compras (Paso 4, 7, 13) o Analista (Paso 6, 9, 12, 17)
                {
                    if (isJefe) // Jefe de Compras
                    {
                        if (tieneReserva && tieneOrdenCompra) // Paso 13
                        {
                            // Regresa al Paso 12: Analista (O/C)
                            etapaPrevio = 9; // Compras
                            estatusPrevio = 4; // Enviado
                        }
                        else if (tieneCotizador) // Paso 7
                        {
                            // Regresa al Paso 6: Analista (Asignación)
                            etapaPrevio = 9; // Compras
                            estatusPrevio = 4; // Enviado
                        }
                        else // Paso 4
                        {
                            // Regresa al Paso 3: DGAF Vo. Bo. (primera vuelta)
                            etapaPrevio = 2; // Dir. ADM
                            estatusPrevio = 4; // Enviado
                        }
                    }
                    else // Analista de Compras
                    {
                        if (tieneReserva && tieneOrdenCompra && haPasadoPorPresidencia) // Paso 17
                        {
                            // Regresa al Paso 16: Legal (primera vuelta)
                            etapaPrevio = 4; // Legal
                            estatusPrevio = 4; // Enviado
                        }
                        else if (tieneReserva && tieneOrdenCompra) // Paso 12
                        {
                            // Regresa al Paso 11: SAP
                            etapaPrevio = 5; // SAP
                            estatusPrevio = 4; // Enviado
                        }
                        else if (tieneReserva) // Paso 9
                        {
                            // Regresa al Paso 8: Presupuesto (Separar Partida)
                            etapaPrevio = 3; // Presupuesto
                            estatusPrevio = 4; // Enviado
                        }
                        else // Paso 6
                        {
                            // Regresa al Paso 5: Supervisor (Asignación de cotizador)
                            etapaPrevio = 8; // Cotizador
                            estatusPrevio = 4; // Enviado
                        }
                    }
                }
                else if (userRoleF == 8) // Paso 5: Supervisor de Cotizadores
                {
                    // Regresa al Paso 4: Jefe de Compras (Firma)
                    etapaPrevio = 9; // Compras
                    estatusPrevio = 4; // Enviado
                }
                else if (userRoleF == 3) // Presupuesto (Paso 8, Paso 10)
                {
                    if (tieneReserva) // Paso 10
                    {
                        // Regresa al Paso 9: Analista (Selección de definitivo)
                        etapaPrevio = 9; // Compras
                        estatusPrevio = 4; // Enviado
                    }
                    else // Paso 8
                    {
                        // Regresa al Paso 7b: DGAF Vo. Bo. (segunda vuelta)
                        etapaPrevio = 2; // Dir. ADM
                        estatusPrevio = 4; // Enviado
                    }
                }
                else if (userRoleF == 5) // Paso 11: SAP
                {
                    // Regresa al Paso 10: Presupuesto (Bloqueo de Partida)
                    etapaPrevio = 3; // Presupuesto
                    estatusPrevio = 4; // Enviado
                }
                else if (userRoleF == 7) // Paso 15: Presidencia
                {
                    // Regresa al Paso 14: DGAF Vo. Bo. (tercera vuelta)
                    etapaPrevio = 2; // Dir. ADM
                    estatusPrevio = 4; // Enviado
                }
                else if (userRoleF == 4) // Legal (Paso 16, Paso 18)
                {
                    if (haPasadoComprasDespuesDePresidencia) // Paso 18
                    {
                        // Regresa al Paso 17: Analista
                        etapaPrevio = 9; // Compras
                        estatusPrevio = 4; // Enviado
                    }
                    else // Paso 16
                    {
                        // Regresa al Paso 15: Presidencia
                        etapaPrevio = 7; // Presidencia
                        estatusPrevio = 4; // Enviado
                    }
                }
                else if (userRoleF == 6) // Paso 19: Control Fiscal
                {
                    // Regresa al Paso 18: Legal (segunda vuelta)
                    etapaPrevio = 4; // Legal
                    estatusPrevio = 4; // Enviado
                }            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "ObtenerPasoPrevio", "Error al calcular el paso previo de la solicitud");
            }
        }
    }
}
