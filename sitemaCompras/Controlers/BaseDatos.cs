namespace sitemaCompras.Controlers
{
    using System;
    using System.Configuration;
    using System.Data;
    using MySql.Data.MySqlClient;

    public partial class BaseDatos
    {
    	private MySqlConnection Cn;

    	private MySqlCommand Cmd;

    	private MySqlDataAdapter Adap;

    	private DataSet ds;

    	public BaseDatos()
    	{
    		Cn = new MySqlConnection();
    		Cn.ConnectionString = ConfigurationManager.ConnectionStrings["compras_ConnectionString"].ConnectionString;
    		Cmd = new MySqlCommand();
    		Cmd.Connection = Cn;
    		Cmd.CommandTimeout = 300;
    	}

    	public bool actualizarPassword(string idsusario, string passnueva)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "actualizarPassword";
    			Cmd.Parameters.Add(new MySqlParameter("@id", idsusario));
    			Cmd.Parameters.Add(new MySqlParameter("@passw", passnueva));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public string[] datosArchivosSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosArchivosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[4]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idArchivos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["nombres"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["archivos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idSolicitudes"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosComentariuosSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosComentariuosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[10]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idComentariuos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["comentario"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idSolicitudes"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEtapas"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idUsuaruios"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["fecha"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["etapa"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["estatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["tipo"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosDetallesSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosDetallesSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[9]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idDetalles_Solicitudes"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["titulos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["renglones"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["cantidades"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["unidades"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["unitario"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["total"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idSolicitudes"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosDirectoresAdministrativos(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosDirectoresAdministrativos";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[4]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idDirectores"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["nombres"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["apellidos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosEstatusSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosEstatusSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[2]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosEstatusUsuarios(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosEstatusUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[2]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosProveedores(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosProveedores";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[4]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idProveedores"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["nombres"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["correos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["telefonos"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[15]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idSolicitudes"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["solicitudBienes"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["numResolucion"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["titulo"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripcion"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["fechas"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEtapas"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idUsuaruios"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idDirectores"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idProveedores"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["precios"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["ordenCompras"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idCotizadores"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["Aprobador"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosTipoSolicitudes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosTipoSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[2]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idTipo"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosTipoUsuario(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosTipoUsuario";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[2]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idTipo"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosUnidadesSolicitantes(int id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosUnidadesSolicitantes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[2]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idUnidades"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["descripciones"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string[] datosUsuarios(string id)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "datosUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			string[] resultado = new string[10]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["idUsuaruios"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["nombres"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["apellidos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idEstatus"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idUnidades"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idTipo"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["telefonos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["emails"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idAutorizaciones"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["campo"])
    			};
    			if (resultado != null)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public int? dirAdmActual()
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dirAdmActual";
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			int? resultado = Convert.ToInt32(ds.Tables[0].Rows[0]["idDirectores"]);
    			if (resultado.HasValue)
    			{
    				return resultado;
    			}
    			resultado = null;
    			Cmd.Connection.Close();
    			return resultado;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public DataSet dLiataProveedores()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dLiataProveedores";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet reporteBody(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "reporteBody";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaEstatusSolicitudes()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaEstatusSolicitudes";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaReporteFecha(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaReporteFecha";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaEstatusUsuarios()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaEstatusUsuarios";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaCotizadores()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaCotizadores";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaEtapaSolicitudes()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaEtapaSolicitudes";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dlistaSolicitudesTipoSolucitudes()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dlistaSolicitudesTipoSolucitudes";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaTipoSolicitudes()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaTipoSolicitudes";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaTipoUsuario()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaTipoUsuario";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaUnidadesSolicitantes()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaUnidadesSolicitantes";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet dListaDirAdm()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaDirAdm";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaProveedores()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaProveedores";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroDirectoresAdministrativos(int? id, string nombre, string apellido, int? estatus)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroDirectoresAdministrativos";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroSolicitudes(int? id, string solicitudBien, string resolucion, string titulos, int? estatus, int? etapa, string usuaruio, string cotizador, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@cotizador", cotizador));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@director", director));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedors", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@desde", desde));
    			Cmd.Parameters.Add(new MySqlParameter("@hasta", hasta));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroSolicitudesCotizadorEnproceso(int? id, string solicitudBien, string resolucion, string titulos, int? estatus, int? etapa, string usuaruio, string cotizador, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroSolicitudesCotizadorEnproceso";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@cotizador", cotizador));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@director", director));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedors", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@desde", desde));
    			Cmd.Parameters.Add(new MySqlParameter("@hasta", hasta));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroSolicitudesEnviados(int? id, string solicitudBien, string resolucion, string titulos, int? estatus, int? etapa, string usuaruio, int? unidad, int? director, int? proveedor, string cotizador, DateTime? desde, DateTime? hasta)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroSolicitudesEnviados";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@director", director));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedor", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@cotizador", cotizador));
    			Cmd.Parameters.Add(new MySqlParameter("@desde", desde));
    			Cmd.Parameters.Add(new MySqlParameter("@hasta", hasta));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroSolicitudesUnidadEnProceso(int? id, string solicitudBien, string resolucion, string titulos, int? estatus, string usuaruio, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroSolicitudesUnidadEnProceso";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@director", director));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedor", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@desde", desde));
    			Cmd.Parameters.Add(new MySqlParameter("@hasta", hasta));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet filtroUsuarios(string id, string nombre, string apellido, int? estatus, int? unidad, int? tipo)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "filtroUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public bool gestionarArchivosSolicitudes(int? id, string nombre, string archivo, int solicitud, string usuario, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarArchivosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@archivo", archivo));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitud", solicitud));
    			Cmd.Parameters.Add(new MySqlParameter("@usuario", usuario));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarAutorizacionUser(string id, int tipo)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarAutorizacionUser";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarComentariuosSolicitudes(int? id, string comentarios, int solicitud, int etapa, int estatus, string usuaruio, int tipo, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarComentariuosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@comentarios", comentarios));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitud", solicitud));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarDetallesSolicitudes(int? id, string titulo, int? renglon, string cantidad, string unidad, string descripcion, string unitarios, string totales, int solicitud, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarDetallesSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@titulo", titulo));
    			Cmd.Parameters.Add(new MySqlParameter("@renglon", renglon));
    			Cmd.Parameters.Add(new MySqlParameter("@cantidad", cantidad));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@unitarios", unitarios));
    			Cmd.Parameters.Add(new MySqlParameter("@totales", totales));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitud", solicitud));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarDirectoresAdministrativos(int? id, string nombre, string apellido, int estatus, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarDirectoresAdministrativos";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarEstatusActualSolicitudes(int id, int estatus)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarEstatusActualSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarEstatusSolicitudes(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarEstatusSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarEstatusUsuarios(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarEstatusUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarEtapaActualSolicitudes(int? id, int etapa)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarEtapaActualSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarEtapasSolicitudes(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarEtapasSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarProveedores(int? id, string nombre, string correo, string telefono, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarProveedores";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@correo", correo));
    			Cmd.Parameters.Add(new MySqlParameter("@telefono", telefono));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarProveedorSolicitudes(int? id, int proveedor, float? precio, string resolucion, string ordenCompra)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarProveedorSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedor", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@precio", precio));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@ordenCompra", ordenCompra));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarSolicitudes(int? id, string resolucion, string titulos, string descripciones, DateTime? fecha, int estatus, int etapa, string usuaruio, int? proveedor, string solicitudBien, float? precio, string ordenCompra, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@descripciones", descripciones));
    			Cmd.Parameters.Add(new MySqlParameter("@fecha", fecha));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedor", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@precio", precio));
    			Cmd.Parameters.Add(new MySqlParameter("@ordenCompra", ordenCompra));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public DataSet salvarSolicitudes(int? id, string resolucion, string titulos, string descripciones, DateTime fecha, int estatus, int etapa, string usuaruio, int? proveedor, string solicitudBien, float? precio, string ordenCompra, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@titulos", titulos));
    			Cmd.Parameters.Add(new MySqlParameter("@resolucion", resolucion));
    			Cmd.Parameters.Add(new MySqlParameter("@descripciones", descripciones));
    			Cmd.Parameters.Add(new MySqlParameter("@fecha", fecha));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@proveedor", proveedor));
    			Cmd.Parameters.Add(new MySqlParameter("@solicitudBien", solicitudBien));
    			Cmd.Parameters.Add(new MySqlParameter("@precio", precio));
    			Cmd.Parameters.Add(new MySqlParameter("@ordenCompra", ordenCompra));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			Adap = new MySqlDataAdapter(Cmd);
    			ds = new DataSet();
    			Adap.Fill(ds);
    			DataSet ejecucion = ((ds == null) ? null : ds);
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public bool gestionarCotizadorSolicitudes(int? id, string idCotizador, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarCotizadorSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@idCotizador", idCotizador));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarTipoSolucitudes(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarTipoSolucitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarTipoSolucitudesSolicitud(int? id, int tipo, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarTipoSolucitudesSolicitud";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarTipoUsuario(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarTipoUsuario";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarUnidadesSolicitantes(int? id, string descripcion, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarUnidadesSolicitantes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@descripcion", descripcion));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool gestionarUsuarios(string id, string idNew, string nombre, string apellido, string pass, string telefono, string email, int? estatus, int unidad, int tipo, int proceso)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@idNew", idNew));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
    			Cmd.Parameters.Add(new MySqlParameter("@pass", pass));
    			Cmd.Parameters.Add(new MySqlParameter("@email", email));
    			Cmd.Parameters.Add(new MySqlParameter("@telefono", telefono));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@unidad", unidad));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Cmd.Parameters.Add(new MySqlParameter("@proceso", proceso));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception ex)
    		{
    			if (Cmd.Connection.State == System.Data.ConnectionState.Open)
    				Cmd.Connection.Close();
    			throw new Exception("gestionarUsuarios: " + ex.Message, ex);
    		}
    	}

    	public bool asignarEstatus(int id, int etapa, int? estatus, string coment, string userN)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "asignarEstatus";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@coment", coment));
    			Cmd.Parameters.Add(new MySqlParameter("@userN", userN));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool siguienteEtapa(int id, int etapa, int? estatus, string coment, string userN)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "siguienteEtapa";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@etapa", etapa));
    			Cmd.Parameters.Add(new MySqlParameter("@estatus", estatus));
    			Cmd.Parameters.Add(new MySqlParameter("@coment", coment));
    			Cmd.Parameters.Add(new MySqlParameter("@userN", userN));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public bool TransferirSB(int id, int idTransfer)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "TransferirSB";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@idTransfer", idTransfer));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	public DataSet infoDirectoresAdministrativos(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "infoDirectoresAdministrativos";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet infoSolicitudes(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "infoSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet infoUsuarios(string id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "infoUsuarios";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaArchivosSolicitudes(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaArchivosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaComentariosSolicitudes(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaComentariosSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaDetallesSolicitudes(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaDetallesSolicitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public DataSet listaSolicitudesTipoSolucitudes(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaSolicitudesTipoSolucitudes";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	public (bool, string[]) login(string id, string passw)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "login";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@passw", passw));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			bool condicion = false;
    			string[] resultado = new string[4]
    			{
    				Convert.ToString(ds.Tables[0].Rows[0]["nombres"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["apellidos"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idTipo"]),
    				Convert.ToString(ds.Tables[0].Rows[0]["idUnidades"])
    			};
    			if (resultado != null)
    			{
    				condicion = true;
    				return (condicion, resultado);
    			}
    			resultado = null;
    			condicion = false;
    			Cmd.Connection.Close();
    			return (condicion, resultado);
    		}
    		catch (Exception)
    		{
    			return (false, null);
    		}
    	}

    	public int ultimoRenglon(int id)
    	{
    		try
    		{
    			int num = 0;
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "ultimoRenglon";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			if (ds != null)
    			{
    				num = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
    			}
    			return num;
    		}
    		catch (Exception)
    		{
    			return 0;
    		}
    	}

    	public string verificarNombreArchivo(int id, string nombre)
    	{
    		try
    		{
    			string nomb = null;
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "verificarNombreArchivo";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			if (ds != null)
    			{
    				nomb = Convert.ToString(ds.Tables[0].Rows[0][0].ToString());
    			}
    			return nomb;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string cantidadReplicas(int id, int tipo)
    	{
    		try
    		{
    			string cantidad = null;
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "cantidadReplicas";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			if (ds != null)
    			{
    				cantidad = Convert.ToString(ds.Tables[0].Rows[0][0].ToString());
    			}
    			return cantidad;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public string crearReplicaSolicitud(int id, string usuaruio, int tipo)
    	{
    		try
    		{
    			string ejecucion = null;
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "crearReplicaSolicitud";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Cmd.Parameters.Add(new MySqlParameter("@usuaruio", usuaruio));
    			Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
    			ds = new DataSet();
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			if (ds != null)
    			{
    				ejecucion = Convert.ToString(ds.Tables[2].Rows[0][0].ToString());
    			}
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	public DataSet dListaReplicasSB(int id)
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "dListaReplicasSB";
    			Cmd.Parameters.Add(new MySqlParameter("@id", id));
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}

    	// ===== MÉTODOS PARA GESTIÓN DE PLANTILLAS DE SOLICITUD (.docx) =====

    	/// <summary>
    	/// Guarda una nueva plantilla .docx como la plantilla activa.
    	/// Desactiva automáticamente las plantillas anteriores.
    	/// </summary>
    	public bool guardarPlantillaSolicitud(string nombreArchivo, string rutaArchivo, string usuario)
    	{
    		try
    		{
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "gestionarPlantillaSolicitud";
    			Cmd.Parameters.Add(new MySqlParameter("@p_id", null));
    			Cmd.Parameters.Add(new MySqlParameter("@p_nombreArchivo", nombreArchivo));
    			Cmd.Parameters.Add(new MySqlParameter("@p_rutaArchivo", rutaArchivo));
    			Cmd.Parameters.Add(new MySqlParameter("@p_subidoPor", usuario));
    			Cmd.Parameters.Add(new MySqlParameter("@p_proceso", 1));
    			Cmd.Connection.Open();
    			int d = Cmd.ExecuteNonQuery();
    			bool ejecucion = d > 0;
    			Cmd.Connection.Close();
    			return ejecucion;
    		}
    		catch (Exception)
    		{
    			return false;
    		}
    	}

    	/// <summary>
    	/// Obtiene la ruta relativa de la plantilla .docx activa.
    	/// Retorna null si no hay plantilla activa.
    	/// </summary>
    	public string obtenerPlantillaActiva()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "obtenerPlantillaActiva";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    			{
    				return Convert.ToString(ds.Tables[0].Rows[0]["rutaArchivo"]);
    			}
    			return null;
    		}
    		catch (Exception)
    		{
    			return null;
    		}
    	}

    	/// <summary>
    	/// Retorna el historial completo de plantillas subidas.
    	/// </summary>
    	public DataSet listaPlantillasSolicitud()
    	{
    		try
    		{
    			ds = new DataSet();
    			Cmd.Parameters.Clear();
    			Cmd.CommandType = CommandType.StoredProcedure;
    			Cmd.CommandText = "listaPlantillasSolicitud";
    			Adap = new MySqlDataAdapter(Cmd);
    			Adap.Fill(ds);
    		}
    		catch (Exception)
    		{
    		}
    		return ds;
    	}
    }

}

