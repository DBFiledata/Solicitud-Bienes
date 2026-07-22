namespace sitemaCompras.viewsAdmin
{
	using System;
	using System.Data;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Web;
	using System.Web.Security;
	using System.Web.UI;
	using System.Web.UI.HtmlControls;
	using System.Web.UI.WebControls;
	using AjaxControlToolkit;
	using sitemaCompras.Controlers;
	using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
	using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
	using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
	using Telerik.Windows.Documents.Spreadsheet.Model;

	public partial class busquedaDocumentos : Page
	{
		private BaseDatos odbusuario;
		private Mailer mails;
		private ReporteCompras reporteCompra;

		protected ScriptManager ScriptManager1;
		protected HiddenField HiddenField1;
		protected ModalPopupExtender mpeEditarArchivo;
		protected HiddenField HiddenField2;
		protected ModalPopupExtender mpeEliminarArchivo;
		protected HiddenField HiddenField3;
		protected ModalPopupExtender mppeRegistrarArchivo;
		protected HiddenField HiddenField4;
		protected ModalPopupExtender mpeEliminarRenglon;
		protected HiddenField HiddenField5;
		protected ModalPopupExtender mpeEditRenglon;
		protected HiddenField HiddenField6;
		protected ModalPopupExtender mpeEliminarSolicitud;
		protected HiddenField HiddenField7;
		protected ModalPopupExtender mpeEstatusEspeciales;
		protected HiddenField HiddenField8;
		protected ModalPopupExtender mpeEtapas;
		protected HiddenField HiddenField9;
		protected ModalPopupExtender mpeComfirmarProceso;
		protected HiddenField HiddenField10;
		protected ModalPopupExtender mpeCrearProveedor;
		protected HiddenField HiddenField11;
		protected ModalPopupExtender mpeCotizadores;
		protected HiddenField HiddenField12;
		protected ModalPopupExtender mpeConfirmarCotizador;
		protected HiddenField HiddenField13;
		protected ModalPopupExtender mpeReporteFecha;
		protected HiddenField HiddenField14;
		protected ModalPopupExtender mpeReplicaResolucion;
		protected HiddenField HiddenField15;
		protected ModalPopupExtender mpeResultadoReplica;
		protected HiddenField HiddenField16;
		protected ModalPopupExtender mpeTransferir;
		protected HiddenField HiddenField17;
		protected ModalPopupExtender mpeConfirmarTransferir;
		protected Panel panelConfirmarTransferir;
		protected Button btnCloseConfirmarTransferir;
		protected Panel panel10;
		protected Label Label75;
		protected DropDownList dlistReplicaSB;
		protected Button btnTransferirReplica;
		protected Panel panelTransferir;
		protected Button btnCloseTransferir;
		protected HtmlGenericControl div9;
		protected GridView gridContenidoTransferir;
		protected Panel PanelReplicaResultado;
		protected Button closeReplicaResultado;
		protected Panel panel9;
		protected Label msjReplicaResultado;
		protected Label Label76;
		protected Label lblResultadoReplica;
		protected Panel PanelReplica;
		protected Button closeReplica;
		protected Panel panel8;
		protected Label msjReplica;
		protected Label idReplicaresolucion;
		protected Label lblCantidad;
		protected Button btnConfirmarReplica;
		protected Panel panelReporteFecha;
		protected Button btnCloseRFecha;
		protected Label Label55;
		protected Label lblFUnidad;
		protected Label Label72;
		protected Label lblFSB;
		protected Label Label56;
		protected Label lblFDirAdm;
		protected Label Label57;
		protected Label lblFPresupuesto;
		protected Label Label61;
		protected Label lblFLega;
		protected Label Label63;
		protected Label lblFSAP;
		protected Label Label65;
		protected Label lblFCFiscal;
		protected Label Label62;
		protected Label lblFPresidencia;
		protected Label Label66;
		protected Label lblFVPresidencia;
		protected Label Label68;
		protected Label lblFSecretaria;
		protected Label Label70;
		protected Label lblFCompras;
		protected Label Label64;
		protected Label lblFontrataciones;
		protected Label Label58;
		protected Label lblFEDocU;
		protected Label Label59;
		protected Label lblFECocP;
		protected Label Label60;
		protected Label lblFPrecioR;
		protected Label Label67;
		protected Label lblFCLinea;
		protected Label Label71;
		protected Label lblFBloqueo;
		protected Label Label73;
		protected Label lblFRechazado;
		protected Label Label69;
		protected Label lblFReservaP;
		protected Label Label74;
		protected Label lblFAnulado;
		protected Panel panelAsignarCotizador;
		protected Button closeAsignarCotizador;
		protected Panel panelListaCotizadores;
		protected DropDownList dlistCotizadores;
		protected Button Button4;
		protected Label Label34;
		protected Panel panelConfirmarCotizador;
		protected Button closeConfirmarCotizador;
		protected Panel panel4;
		protected Label Label36;
		protected Label lblCotizadorSelect;
		protected Button btnConfirmarCotizador;
		protected Panel panelCrearProveedor;
		protected Button closeCrearProveedor;
		protected Label Label40;
		protected TextBox txtNombreProveedorCrear;
		protected Label Label41;
		protected TextBox txtCorreoProveedorCrear;
		protected Label Label42;
		protected TextBox txtTelefonoProveedorCrear;
		protected Button btnCrearProveedor;
		protected Panel panelRegistrarArchivo;
		protected Button btnRegistrarArchivoClose;
		protected Label Label20;
		protected FileUpload FileUpload1;
		protected Label Label18;
		protected TextBox txtNombreArchivo;
		protected Button btnRegistrarArchivo;
		protected Panel panelEditarRenglon;
		protected Button btnEditarRenglonClose;
		protected Label Label8;
		protected TextBox txtRenglonEdit;
		protected Label Label19;
		protected TextBox txtCantidadEdit;
		protected Label Label21;
		protected TextBox txtUnidadContentEdit;
		protected Label Label22;
		protected TextBox txtDecsContentEdit;
		protected Label Label23;
		protected TextBox txtCostoEdit;
		protected Button btnActualizarRenglon;
		protected Panel panelEditar;
		protected Button btnEditarClose;
		protected Label Label14;
		protected TextBox txtEditNombre;
		protected Label Label15;
		protected TextBox txtEditApellido;
		protected Label Label16;
		protected DropDownList dListEditEstado;
		protected Button btnEditar;
		protected Panel panelEliminarArchivo;
		protected Button btnEliminarArchivoClose;
		protected Label lblElimiarReg;
		protected Label Label17;
		protected Label lblDeleteNombreArchivo;
		protected Button btnEliminarArchivo;
		protected Panel panelEliminarRenglon;
		protected Button btnEliminarRenglonClose;
		protected Label Label5;
		protected Label Label6;
		protected Label lblRenglon;
		protected Button btnEliminarRenglon;
		protected Panel panelEliminarSolicitud;
		protected Button btnEliminarSolicitudSolicitud;
		protected Label Label27;
		protected Label Label28;
		protected Label lblEliminarSolicitud;
		protected Label Label29;
		protected Label lblTituloEliminar;
		protected Button btnEliminarSolicitud;
		protected Panel panelEstatusEspeciales;
		protected Button btnCloseEstatusEspeciales;
		protected Panel panel6;
		protected Button btnEDocUnidad;
		protected Button btnEDocProveedor;
		protected Button btnPrecioR;
		protected Button btnCotizacionL;
		protected Button btnBloqueo;
		protected Button btnRechazado;
		protected Button Button5;
		protected Button Button6;
		protected Label Label30;
		protected Panel panelEtapa;
		protected Button btnCloseEtapa;
		protected Panel panel5;
		protected Button btnUnidad;
		protected Button btnDirAdm;
		protected Button btnpresupuesto;
		protected Button btnLegal;
		protected Button btnSap;
		protected Button btnControlF;
		protected Button btnPresidencia;
		protected Button btnVPresidencia;
		protected Button btnSecretaria;
		protected Button btnCompras;
		protected Button btnContrataciones;
		protected Label Label31;
		protected Panel panelConfirmarProceso;
		protected Button closeConfirmarProceso;
		protected Panel panel7;
		protected Label Label33;
		protected TextBox txtComentario;
		protected Button btnConfirmarPorceso;
		protected MultiView MultiView1;
		protected View ViewTabla;
		protected Panel Panel1;
		protected Label Label38;
		protected TextBox txtId;
		protected Label Label43;
		protected TextBox txtSolicitudBien;
		protected Label Label44;
		protected TextBox txtTituloF;
		protected Label Label54;
		protected TextBox txtNumResolucion;
		protected HtmlGenericControl divEstEta;
		protected Label Label49;
		protected DropDownList dlistEstatus;
		protected Label Label48;
		protected DropDownList dListEtapa;
		protected HtmlGenericControl divProCot;
		protected Label Label45;
		protected DropDownList dlistCotizador;
		protected Label Label46;
		protected DropDownList dlistProveedorF;
		protected HtmlGenericControl divUniUser;
		protected Label Label50;
		protected DropDownList dlistUnidad;
		protected Label Label51;
		protected DropDownList dlistUsuario;
		protected Label Label52;
		protected TextBox txtDesde;
		protected Label Label53;
		protected TextBox TxtHasta;
		protected Button btnFiltrar;
		protected GridView GridView1;
		protected View viewDescripciones;
		protected Panel panelInfoGeneral1;
		protected HtmlGenericControl divNumEditar;
		protected Label lblFicha;
		protected Label txtUnidad;
		protected Label lblEtapa;
		protected TextBox txtTitulo;
		protected Label Label1;
		protected Label txtSolicitante;
		protected Panel panelInfoGeneral2;
		protected HtmlGenericControl div1;
		protected Label Label2;
		protected TextBox txtSB;
		protected Label Label3;
		protected TextBox txtFecha;
		protected Panel panelInfoGeneral3;
		protected Panel panelInfoGeneral3_1;
		protected Label Label4;
		protected TextBox txtDescripcion;
		protected Panel panelInfoGeneral3_2;
		protected Label Label47;
		protected CheckBoxList checkTipoSol;
		protected Button btnActualizar;
		protected Panel panelInfoPreveedor;
		protected Label headNameCotizador;
		protected HtmlGenericControl div2;
		protected Label Label7;
		protected Label lblProveedor;
		protected Label Label24;
		protected Label lblPrecioProveedor;
		protected Label Label25;
		protected Label lblResolucion;
		protected Label Label26;
		protected Label lblOrdenCompra;
		protected LinkButton LbReplicaResolucion1;
		protected System.Web.UI.WebControls.Image Image10;
		protected Panel panelAsignarProveedor;
		protected HtmlGenericControl div7;
		protected Label Label32;
		protected DropDownList dlistProveedor;
		protected Label Label35;
		protected TextBox txtPrecioProveedor;
		protected Label Label37;
		protected TextBox txtResolucion;
		protected Label Label39;
		protected TextBox txtOrdenCompra;
		protected LinkButton lbActualizarProveedor;
		protected System.Web.UI.WebControls.Image Image1;
		protected LinkButton lbCrearProveedor;
		protected System.Web.UI.WebControls.Image Image3;
		protected LinkButton LbReplicaResolucion;
		protected System.Web.UI.WebControls.Image Image6;
		protected Panel panelBtnVer;
		protected Button btnVerdoc2;
		protected Panel panelBtns;
		protected Button btnVerDoc1;
		protected Button btnEnviar;
		protected Panel panelBtnsOthers;
		protected Button btnSiguienteEtapa;
		protected Button btnVerDoc;
		protected Button btnEstatusEspeciales;
		protected LinkButton LbReplicaSB;
		protected System.Web.UI.WebControls.Image Image5;
		protected LinkButton LbTransferRenglones;
		protected System.Web.UI.WebControls.Image Image7;
		protected Panel panelAddContenido;
		protected HtmlGenericControl div4;
		protected Label Label13;
		protected TextBox txtRenglon;
		protected Label Label9;
		protected TextBox txtCantidad;
		protected Label Label10;
		protected TextBox txtUnidadContent;
		protected Label Label11;
		protected TextBox txtDecsContent;
		protected Label Label12;
		protected TextBox txtCosto;
		protected LinkButton linkAddRenglon;
		protected System.Web.UI.WebControls.Image Image4;
		protected HtmlGenericControl div3;
		protected GridView gridContenido;
		protected Panel Panel2;
		protected Button btnAddREg;
		protected HtmlGenericControl div5;
		protected GridView gridArchivo;
		protected Panel panelMensajes;
		protected UpdatePanel uPanelChat;
		protected LinkButton lbtnContorlMensajes;
		protected System.Web.UI.WebControls.Image imgEncendido;
		protected HtmlGenericControl div6;
		protected Panel chatArea;
		protected Panel ChatHistory;
		protected Panel panelChat;
		protected TextBox txtMsj;
		protected LinkButton linkBMsj;
		protected System.Web.UI.WebControls.Image Image2;
		protected View viewPDF;
		protected Button btnCloseContent;
		protected Literal ltEmbed;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.IsPostBack)
			{
				return;
			}
			if (!User.Identity.IsAuthenticated)
			{
				Response.Redirect("~/Login.aspx");
			}
			// Acceso para Administrador (1) y Admin User (7)
			else if ((Convert.ToInt32(Session["type"]) == 1) | (Convert.ToInt32(Session["type"]) == 7))
			{
				DataSet ds = new DataSet();
				ds = filtros(null, "", "", "", null, null, "", null, null, null, null, null, null);
				if (ds != null && ds.Tables.Count > 0)
				{
					GridView1.DataSource = ds;
				}
				else
				{
					GridView1.DataSource = new DataTable();
				}
				GridView1.DataBind();
				MultiView1.ActiveViewIndex = 0;

				odbusuario = new BaseDatos();
				DataSet ds2 = odbusuario.dLiataProveedores();
				dlistProveedor.Items.Clear();
				dlistProveedor.DataSource = ds2;
				dlistProveedor.DataBind();
				dlistProveedor.Items.Insert(0, "\ud83d\udcac Seleccionar");

				odbusuario = new BaseDatos();
				DataSet ds3 = odbusuario.dListaCotizadores();
				dlistCotizadores.Items.Clear();
				dlistCotizadores.DataSource = ds3;
				dlistCotizadores.DataBind();
				dlistCotizadores.Items.Insert(0, "\ud83d\udc77\u200d\u2642\ufe0f Seleccionar");
				dlistCotizador.Items.Clear();
				dlistCotizador.DataSource = ds3;
				dlistCotizador.DataBind();
				dlistCotizador.Items.Insert(0, "\ud83d\udc77\u200d\u2642\ufe0f Seleccionar");
				dlistProveedorF.Items.Clear();
				dlistProveedorF.DataSource = ds2;
				dlistProveedorF.DataBind();
				dlistProveedorF.Items.Insert(0, "\ud83e\udd35\u200d\u2642\ufe0f Seleccionar");

				odbusuario = new BaseDatos();
				DataSet ds4 = odbusuario.dListaEstatusSolicitudes();
				dlistEstatus.Items.Clear();
				dlistEstatus.DataSource = ds4;
				dlistEstatus.DataBind();
				dlistEstatus.Items.Insert(0, "\u2699\ufe0f Seleccionar");

				odbusuario = new BaseDatos();
				DataSet ds5 = odbusuario.dListaEtapaSolicitudes();
				dListEtapa.Items.Clear();
				dListEtapa.DataSource = ds5;
				dListEtapa.DataBind();
				dListEtapa.Items.Insert(0, "\ud83d\udd00 Seleccionar");

				odbusuario = new BaseDatos();
				DataSet ds6 = odbusuario.dListaUnidadesSolicitantes();
				dlistUnidad.Items.Clear();
				dlistUnidad.DataSource = ds6;
				dlistUnidad.DataBind();
				dlistUnidad.Items.Insert(0, "\ud83d\udccd Seleccionar");

				// El admin ve TODO: proveedores, etapas/estatus especiales y botones full
				panelAsignarProveedor.Enabled = true;
				panelAsignarProveedor.Visible = true;
				panelBtnsOthers.Enabled = true;
				panelBtnsOthers.Visible = true;
				panelBtnVer.Visible = false;
				panelBtns.Visible = false;
				LbReplicaResolucion.Visible = true;
				LbReplicaSB.Visible = true;
				LbTransferRenglones.Visible = true;
			}
			else
			{
				Session.Abandon();
				Session.Clear();
				FormsAuthentication.SignOut();
				Response.Redirect("~/login.aspx");
			}
		}

		private int GetColumnIndexByName(GridView grid, string columnName)
		{
			for (int i = 0; i < grid.Columns.Count; i++)
			{
				if (grid.Columns[i].HeaderText.ToLowerInvariant().Trim() == columnName.ToLowerInvariant().Trim())
				{
					return i;
				}
			}
			return -1;
		}

		protected DataSet filtros(int? id, string solicitudBien, string resolucion, string descripciones, int? estatus, int? etapa, string usuaruio, string cotizador, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
		{
			DataSet ds = new DataSet();
			odbusuario = new BaseDatos();
			// Admin ve todas las solicitudes sin restriccion de usuario ni unidad
			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, estatus, etapa, usuaruio, cotizador, unidad, director, proveedor, desde, hasta);
			return ds;
		}

		protected void btnFiltrar_Click(object sender, EventArgs e)
		{
			try
			{
				int? id = null;
				int? estatus = null;
				int? etapa = null;
				int? unidad = null;
				int? director = null;
				int? proveedor = null;
				string solicitudBien = "";
				string resolucion = "";
				string titulo = "";
				string usuaruio = "";
				string cotizador = "";
				DateTime? desde = null;
				DateTime? hasta = null;
				id = VerificarCampoIntNull(txtId);
				estatus = VerificarDList(dlistEstatus);
				etapa = VerificarDList(dListEtapa);
				usuaruio = VerificarDListText(dlistUsuario);
				cotizador = VerificarDListText(dlistCotizador);
				unidad = VerificarDList(dlistUnidad);
				director = null;
				proveedor = VerificarDList(dlistProveedorF);
				solicitudBien = VerificarCampo(txtSolicitudBien);
				resolucion = VerificarCampo(txtNumResolucion);
				titulo = VerificarCampo(txtTituloF);
				desde = VerificarCampoFecha(txtDesde);
				hasta = VerificarCampoFecha(TxtHasta);
				DataSet ds = filtros(id, solicitudBien, resolucion, titulo, estatus, etapa, usuaruio, cotizador, unidad, director, proveedor, desde, hasta);
				GridView1.DataSource = ds;
				GridView1.DataBind();
				MultiView1.ActiveViewIndex = 0;
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnFiltrar_Click", "aplicar filtro búsqueda");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo aplicar filtro');", addScriptTags: true);
		}
		}

		protected string VerificarNulidadFecha(string txt)
		{
			if (!string.IsNullOrEmpty(txt) & (txt != "&nbsp;"))
			{
				return Convert.ToDateTime(txt).ToString("yyyy-MM-dd");
			}
			return "";
		}

		protected string VerificarCampo(TextBox txt)
		{
			if (!string.IsNullOrEmpty(txt.Text))
			{
				return txt.Text;
			}
			return string.Empty;
		}

		protected DateTime? VerificarCampoFecha(TextBox txt)
		{
			if (!string.IsNullOrEmpty(txt.Text))
			{
				return Convert.ToDateTime(txt.Text);
			}
			return null;
		}

		protected int? VerificarCampoIntNull(TextBox txt)
		{
			if (!string.IsNullOrEmpty(txt.Text))
			{
				return Convert.ToInt32(txt.Text);
			}
			return null;
		}

		protected int? VerificarDList(DropDownList list)
		{
			try
			{
				if (list.SelectedValue != "")
				{
					if (Convert.ToInt32(list.SelectedIndex) > 0)
					{
						return Convert.ToInt32(list.SelectedValue);
					}
					return null;
				}
				return null;
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.VerificarDList", "validar dropdown list");
			return 0;
		}
		}

		protected string VerificarDListText(DropDownList list)
		{
			try
			{
				if (list.SelectedValue != "")
				{
					if (Convert.ToInt32(list.SelectedIndex) > 0)
					{
						return Convert.ToString(list.SelectedValue);
					}
					return "";
				}
				return "";
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.VerificarDListText", "validar dropdown text");
			return "";
		}
		}

		protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
				{
					GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion = gvr.Cells[0].Text.ToString().Trim();
					Session["idDoc"] = eleccion;
					MultiView1.ActiveViewIndex = 1;
					txtSolicitante.Text = Convert.ToString(Session["username"]);
					odbusuario = new BaseDatos();
					string[] datos = new string[14];
					datos = odbusuario.datosSolicitudes(Convert.ToInt32(eleccion));
					txtUnidad.Text = gvr.Cells[9].Text.ToString().Trim();
					txtSolicitante.Text = gvr.Cells[8].Text.ToString().Trim();
					txtTitulo.Text = datos[3];
					txtSB.Text = datos[1];
					txtFecha.Text = Convert.ToDateTime(datos[5]).ToString("yyyy-MM-dd");
					txtDescripcion.Text = datos[4];
					Session["resolucionActual"] = datos[2];
					Session["etapaActual"] = datos[7];
					Session["estatusActual"] = datos[6];
					Session["proveedorActual"] = datos[10];
					Session["precioActual"] = datos[11];
					Session["oCompraActual"] = datos[12];
					Session["UsuarioActual"] = datos[8];
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.listaSolicitudesTipoSolucitudes(Convert.ToInt32(eleccion));
					DataTable dt = ds.Tables[0];
					foreach (DataRow r in dt.Rows)
					{
						if (checkTipoSol.Items.FindByValue(Convert.ToString(new ListItem(r[0].ToString(), r[0].ToString()))) != null)
						{
							checkTipoSol.Items.FindByValue(Convert.ToString(new ListItem(r[0].ToString(), r[0].ToString()))).Selected = true;
						}
					}
					odbusuario = new BaseDatos();
					string[] datosProveedor = new string[4];
					datosProveedor = odbusuario.datosProveedores(Convert.ToInt32(datos[10]));
					lblProveedor.Text = datosProveedor[1];
					lblPrecioProveedor.Text = datos[11];
					lblResolucion.Text = datos[2];
					lblOrdenCompra.Text = datos[12];
					odbusuario = new BaseDatos();
					DataSet ds2 = new DataSet();
					ds2 = odbusuario.listaDetallesSolicitudes(Convert.ToInt32(eleccion));
					gridContenido.DataSource = ds2;
					gridContenido.DataBind();
					if (!string.IsNullOrEmpty(datos[13]))
					{
						odbusuario = new BaseDatos();
						DataSet dsCotizador = odbusuario.infoUsuarios(datos[13]);
						headNameCotizador.Text = "Cotizador: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["nombres"]) + " " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["apellidos"]);
						headNameCotizador.ToolTip = "Telefono: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["telefono"]) + "\nEmail: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["email"]);
					}
					odbusuario = new BaseDatos();
					string[] datosProveedor2 = new string[4];
					datosProveedor2 = odbusuario.datosProveedores(Convert.ToInt32(datos[10]));
					dlistProveedor.SelectedValue = datosProveedor2[0];
					txtPrecioProveedor.Text = datos[11];
					txtResolucion.Text = datos[2];
					txtOrdenCompra.Text = datos[12];
					odbusuario = new BaseDatos();
					DataSet ds3 = odbusuario.listaArchivosSolicitudes(Convert.ToInt32(eleccion));
					gridArchivo.DataSource = ds3.Tables[0];
					gridArchivo.DataBind();
					CommandEventArgs commandEventArgs = new CommandEventArgs("editar", "1");
					btnPanelMensajes_Click(null, commandEventArgs);
				}
				if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
				{
					GridViewRow gvr4 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion4 = gvr4.Cells[0].Text.ToString().Trim();
					Session["idDoc"] = eleccion4;
					lblEliminarSolicitud.Text = gvr4.Cells[0].Text.ToString().Trim();
					lblTituloEliminar.Text = gvr4.Cells[2].Text.ToString().Trim();
					mpeEliminarSolicitud.Show();
				}
				if (e.CommandName.Equals("verDoc", StringComparison.CurrentCultureIgnoreCase))
				{
					GridViewRow gvr5 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion5 = gvr5.Cells[0].Text.ToString().Trim();
					Session["idDoc"] = eleccion5;
					DataSet ds4 = new DataSet();
					odbusuario = new BaseDatos();
					ds4 = odbusuario.reporteBody(Convert.ToInt32(eleccion5));
					string eleccion6 = "File_Compras_" + eleccion5;
					// ===== INICIO: Código Telerik Spreadsheet original (comentado - no eliminar) =====
					/*
					reporteCompra = new ReporteCompras();
					Workbook wk = reporteCompra.createexcellficha(ds4, Convert.ToString(ds4.Tables[1].Rows[0]["diradmin"]).ToUpper(), Convert.ToString(ds4.Tables[1].Rows[0]["iddirectores"]).ToUpper(), Convert.ToString(ds4.Tables[1].Rows[0]["Unidad"]).ToUpper());
					IWorkbookFormatProvider XlsformatProvider = new XlsxFormatProvider();
					byte[] renderedBytes = null;
					PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();
					using (MemoryStream ms = new MemoryStream())
					{
						pdfFormatProvider.Export(wk, ms);
						renderedBytes = ms.ToArray();
					}
					File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion6 + ".pdf", renderedBytes);
					string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
					embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
					embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
					embed += "</object>";
					ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion6 + ".pdf"));
					*/
					// ===== FIN: Código Telerik Spreadsheet original =====
					// ===== INICIO: Nuevo flujo con plantilla Word/Excel (ReporteComprasDocx) =====
					string htmlPath = HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion6 + ".html";
					odbusuario = new BaseDatos();
					string templateRuta = odbusuario.obtenerPlantillaActiva();
					string templatePath;
					if (!string.IsNullOrEmpty(templateRuta))
						templatePath = HttpContext.Current.Server.MapPath(templateRuta);
					else
					{
						templatePath = HttpContext.Current.Server.MapPath("~/DocumentsPlantillas/plantilla_default.docx");
						if (!File.Exists(templatePath))
						{
							ReporteComprasDocx generador = new ReporteComprasDocx();
							generador.CrearPlantillaDefault(templatePath, HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png"), HttpContext.Current.Server.MapPath("~/userscontent/img/header.png"), HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png"));
						}
					}
					ReporteComprasDocx reporteDocx = new ReporteComprasDocx();
					reporteDocx.GenerarDocumentoDesdePlantilla(ds4, templatePath, htmlPath);
					string embedHtml = "<iframe src=\"{0}\" width=\"1294px\" height=\"682px\" frameborder=\"0\"></iframe>";
					ltEmbed.Text = string.Format(embedHtml, ResolveUrl("~/DocumentsSolicitudes/" + eleccion6 + ".html") + "?v=" + DateTime.Now.Ticks);
					// ===== FIN: Nuevo flujo con plantilla Word/Excel =====
					MultiView1.ActiveViewIndex = 2;
					btnCloseContent.CommandArgument = "1";
				}
				if (!e.CommandName.Equals("Rfrecha", StringComparison.CurrentCultureIgnoreCase))
				{
					return;
				}
				GridViewRow gvr6 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
				string eleccion7 = gvr6.Cells[0].Text.ToString().Trim();
				Session["idDoc"] = eleccion7;
				odbusuario = new BaseDatos();
				DataSet ds5 = odbusuario.listaReporteFecha(Convert.ToInt32(eleccion7));
				Label[] labels = new Label[20]
				{
					lblFUnidad, lblFDirAdm, lblFPresupuesto, lblFLega, lblFSAP, lblFCFiscal, lblFPresidencia, lblFVPresidencia, lblFSecretaria, lblFCompras,
					lblFontrataciones, lblFEDocU, lblFECocP, lblFPrecioR, lblFCLinea, lblFBloqueo, lblFRechazado, lblFReservaP, lblFAnulado, lblFSB
				};
				string[] columnNames = new string[20]
				{
					"FUnidad", "FDirAdm", "FPresupuesto", "FLega", "FSAP", "FCFiscal", "FPresidencia", "FVPresidencia", "FSecretaria", "FCompras",
					"Fontrataciones", "FEDocU", "FECocP", "FPrecioR", "FCLinea", "FBloqueo", "FRechazado", "FReservaP", "FAnulado", "FSB"
				};
				for (int i = 0; i < labels.Length; i++)
				{
					string columnName = columnNames[i];
					if (ds5.Tables[0].Rows[0][columnName] is DateTime fecha)
					{
						labels[i].Text = fecha.ToString("d-M-yyyy", CultureInfo.GetCultureInfo("es-ES"));
					}
					else
					{
						labels[i].Text = "";
					}
				}
				mpeReporteFecha.Show();
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.GridView1_RowCommand", "editar/ver/eliminar solicitud");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				// Admin siempre puede eliminar
				Button btnEliminar = (Button)e.Row.FindControl("Button4_del");
				if (btnEliminar != null)
					btnEliminar.Visible = true;
			}
		}

		protected void btnEliminarSolicitud_Click(object sender, EventArgs e)
		{
			try
			{
				int id = 0;
				odbusuario = new BaseDatos();
				id = Convert.ToInt32(Session["idDoc"]);
				if (odbusuario.gestionarSolicitudes(id, "", "", "", null, 0, 0, "", 0, "", 0f, "", 3))
				{
					btnFiltrar_Click(null, null);
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Solicitud eliminada');", addScriptTags: true);
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo eliminar la solicitud');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnEliminarSolicitud_Click", "eliminar solicitud");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar la solicitud');", addScriptTags: true);
		}
		}

		protected void btnActualizar_Click(object sender, EventArgs e)
		{
			try
			{
				float valorConvertido1;
				float? precioActual = ((!string.IsNullOrWhiteSpace(Convert.ToString(Session["precioActual"])) && float.TryParse(Convert.ToString(Session["precioActual"]), out valorConvertido1)) ? new int?((int)valorConvertido1) : ((int?)null));
				int valorConvertido2;
				int? proveedorActual = ((!string.IsNullOrWhiteSpace(Convert.ToString(Session["proveedorActual"])) && int.TryParse(Convert.ToString(Session["proveedorActual"]), out valorConvertido2)) ? new int?(valorConvertido2) : ((int?)null));
				int checkeditemscount = checkTipoSol.Items.Cast<ListItem>().Count((ListItem li) => li.Selected);
				int id = 0;
				if ((!string.IsNullOrEmpty(txtUnidad.Text) & !string.IsNullOrEmpty(txtTitulo.Text) & !string.IsNullOrEmpty(txtSolicitante.Text) & !string.IsNullOrEmpty(txtFecha.Text) & !string.IsNullOrEmpty(txtDescripcion.Text)) && checkeditemscount > 0)
				{
					id = Convert.ToInt32(Session["idDoc"]);
					odbusuario = new BaseDatos();
					if (odbusuario.gestionarSolicitudes(id, Convert.ToString(Session["resolucionActual"]), txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(Session["estatusActual"]), Convert.ToInt32(Session["etapaActual"]), Convert.ToString(Session["UsuarioActual"]), proveedorActual, txtSB.Text, precioActual, Convert.ToString(Session["oCompraActual"]), 2))
					{
						odbusuario = new BaseDatos();
						foreach (ListItem lista in checkTipoSol.Items)
						{
							if (lista.Selected)
							{
								odbusuario.gestionarTipoSolucitudesSolicitud(id, Convert.ToInt32(lista.Value), 1);
							}
							else
							{
								odbusuario.gestionarTipoSolucitudesSolicitud(id, Convert.ToInt32(lista.Value), 3);
							}
						}
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Generales de registro actualizado');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo actualizar el registro');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos de el area de generales');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnActualizar_Click", "actualizar solicitud");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo actualizar registro');", addScriptTags: true);
		}
		}

		protected void gridContenido_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName.Equals("editarRenglon", StringComparison.CurrentCultureIgnoreCase))
			{
				try
				{
					GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion = gvr.Cells[0].Text.ToString().Trim();
					Session["idRenglon"] = eleccion;
					txtRenglonEdit.Text = gvr.Cells[1].Text.ToString().Trim();
					txtCantidadEdit.Text = HttpUtility.HtmlDecode(gvr.Cells[2].Text.ToString().Trim());
					txtUnidadContentEdit.Text = HttpUtility.HtmlDecode(gvr.Cells[3].Text.ToString().Trim());
					txtDecsContentEdit.Text = HttpUtility.HtmlDecode(gvr.Cells[4].Text.ToString().Trim());
					txtCostoEdit.Text = HttpUtility.HtmlDecode(gvr.Cells[5].Text.ToString().Trim());
					txtCostoEdit.Text = (string.IsNullOrWhiteSpace(txtCostoEdit.Text) ? "" : txtCostoEdit.Text);
					txtCantidadEdit.Text = (string.IsNullOrWhiteSpace(txtCantidadEdit.Text) ? "" : txtCantidadEdit.Text);
					mpeEditRenglon.Show();
				}
				catch (Exception ex) { ErrorHandler.LogError(ex, "busquedaDocumentos.gridContenido_RowCommand", "editar renglón popup"); }
			}
			if (e.CommandName.Equals("eliminarRenglon", StringComparison.CurrentCultureIgnoreCase))
			{
				try
				{
					GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion2 = gvr2.Cells[0].Text.ToString().Trim();
					lblRenglon.Text = gvr2.Cells[1].Text.ToString().Trim();
					Session["idRenglon"] = eleccion2;
					mpeEliminarRenglon.Show();
				}
				catch (Exception ex) { ErrorHandler.LogError(ex, "busquedaDocumentos.gridContenido_RowCommand", "eliminar renglón popup"); }
			}
		}

		protected void btnAddRenglon_Click(object sender, EventArgs e)
		{
			try
			{
				int id = 0;
				if (!string.IsNullOrEmpty(txtDecsContent.Text))
				{
					odbusuario = new BaseDatos();
					float? total = null;
					float valorConvertido3;
					float? costo = ((!string.IsNullOrWhiteSpace(txtCosto.Text) && float.TryParse(txtCosto.Text, out valorConvertido3)) ? new float?(valorConvertido3) : ((float?)null));
					int valorConvertido4;
					int? renglon = ((!string.IsNullOrWhiteSpace(txtRenglon.Text) && int.TryParse(txtRenglon.Text, out valorConvertido4)) ? new int?(valorConvertido4) : ((int?)null));
					float valorConvertido5;
					float? cantidad = ((!string.IsNullOrWhiteSpace(txtCantidad.Text) && float.TryParse(txtCantidad.Text, out valorConvertido5)) ? new float?(valorConvertido5) : ((float?)null));
					string cantidad2 = Convert.ToString(cantidad);
					string costo2 = Convert.ToString(costo);
					string total2 = null;
					id = Convert.ToInt32(Session["idDoc"]);
					if (!string.IsNullOrEmpty(txtCosto.Text) & !string.IsNullOrEmpty(txtCantidad.Text))
					{
						total = (float)Math.Round(Convert.ToSingle(txtCantidad.Text) * Convert.ToSingle(txtCosto.Text), 2);
						total2 = Convert.ToString(total);
					}
					if (odbusuario.gestionarDetallesSolicitudes(0, txtDecsContent.Text, renglon, cantidad2, txtUnidadContent.Text, txtDecsContent.Text, costo2, total2, id, 1))
					{
						odbusuario = new BaseDatos();
						DataSet ds = odbusuario.listaDetallesSolicitudes(id);
						gridContenido.DataSource = ds;
						gridContenido.DataBind();
						limpiarCamposContenido();
						if (!div3.Visible) div3.Visible = true;
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo salvar el renglon');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnAddRenglon_Click", "agregar renglón");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo agregar renglon');", addScriptTags: true);
		}
		}

		protected void btnEliminarRenglon_Click(object sender, EventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				int id = Convert.ToInt32(Session["idRenglon"]);
				int idDoc = Convert.ToInt32(Session["idDoc"]);
				if (odbusuario.gestionarDetallesSolicitudes(id, "", 0, "", "", "", "", "", 0, 3))
				{
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.listaDetallesSolicitudes(idDoc);
					gridContenido.DataSource = ds;
					gridContenido.DataBind();
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Renglon eliminado');", addScriptTags: true);
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo eliminar el renglon');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnEliminarRenglon_Click", "eliminar renglón");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar renglon');", addScriptTags: true);
		}
		}

		protected void btnActualizarRenglon_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(txtDecsContentEdit.Text))
				{
					odbusuario = new BaseDatos();
					int id = Convert.ToInt32(Session["idRenglon"]);
					int idDoc = Convert.ToInt32(Session["idDoc"]);
					float? total = null;
					float valorConvertido3;
					float? costo = ((!string.IsNullOrWhiteSpace(txtCostoEdit.Text) && float.TryParse(txtCostoEdit.Text, out valorConvertido3)) ? new float?(valorConvertido3) : ((float?)null));
					int valorConvertido4;
					int? renglon = ((!string.IsNullOrWhiteSpace(txtRenglonEdit.Text) && int.TryParse(txtRenglonEdit.Text, out valorConvertido4)) ? new int?(valorConvertido4) : ((int?)null));
					float valorConvertido5;
					float? cantidad = ((!string.IsNullOrWhiteSpace(txtCantidadEdit.Text) && float.TryParse(txtCantidadEdit.Text, out valorConvertido5)) ? new float?(valorConvertido5) : ((float?)null));
					string cantidad2 = Convert.ToString(cantidad);
					string costo2 = Convert.ToString(costo);
					string total2 = null;
					if (!string.IsNullOrEmpty(txtCostoEdit.Text) & (txtCostoEdit.Text != "\u00a0") & !string.IsNullOrEmpty(txtCantidadEdit.Text) & (txtCantidadEdit.Text != "\u00a0"))
					{
						total = (float)Math.Round(Convert.ToSingle(txtCantidadEdit.Text) * Convert.ToSingle(txtCostoEdit.Text), 2);
						total2 = Convert.ToString(total);
					}
					if (odbusuario.gestionarDetallesSolicitudes(id, txtDecsContentEdit.Text, renglon, cantidad2, txtUnidadContentEdit.Text, txtDecsContentEdit.Text, costo2, total2, idDoc, 2))
					{
						odbusuario = new BaseDatos();
						DataSet ds = odbusuario.listaDetallesSolicitudes(idDoc);
						gridContenido.DataSource = ds;
						gridContenido.DataBind();
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Renglon editado');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo editar el renglon');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnActualizarRenglon_Click", "editar renglón");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar renglon');", addScriptTags: true);
		}
		}

		protected void gridArchivo_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName.Equals("eliminarArchivo", StringComparison.CurrentCultureIgnoreCase))
				{
					GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion = gvr.Cells[0].Text.ToString().Trim();
					Session["idArchivo"] = eleccion;
					Session["idArchivoDoc"] = gvr.Cells[2].Text.ToString().Trim();
					lblDeleteNombreArchivo.Text = gvr.Cells[1].Text.ToString().Trim();
					mpeEliminarArchivo.Show();
				}
				if (e.CommandName.Equals("verDoc", StringComparison.CurrentCultureIgnoreCase))
				{
					GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion2 = gvr2.Cells[1].Text.ToString().Trim();
					string archivo = gvr2.Cells[2].Text.ToString().Trim();
					Session["idArchivo"] = eleccion2;
					Session["idArchivoDoc"] = archivo;
					string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
					embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
					embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
					embed += "</object>";
					ltEmbed.Text = string.Format(embed, ResolveUrl("~/Documents/" + archivo + "?v=" + Convert.ToString(DateTime.Now.Ticks)));
					MultiView1.ActiveViewIndex = 2;
					btnCloseContent.CommandArgument = "2";
				}
			}
			catch (Exception ex) { ErrorHandler.LogError(ex, "busquedaDocumentos.gridArchivo_RowCommand", "ver/eliminar archivo adjunto"); }
		}

		protected void gridArchivo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				// Admin siempre puede eliminar archivos
				Button btneliminarArchivo = (Button)e.Row.FindControl("Button11");
				if (btneliminarArchivo != null)
					btneliminarArchivo.Visible = true;
			}
		}

		protected void btnCloseContent_Command(object sender, CommandEventArgs e)
		{
			if (Convert.ToInt32((string)e.CommandArgument) == 1)
			{
				MultiView1.ActiveViewIndex = 0;
			}
			else if (Convert.ToInt32((string)e.CommandArgument) == 2)
			{
				MultiView1.ActiveViewIndex = 1;
			}
		}

		protected void btnAddREg_Click(object sender, EventArgs e)
		{
			try { mppeRegistrarArchivo.Show(); }
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnAddREg_Click", "abrir modal registrar archivo");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
			}
		}

		protected void btnRegistrarArchivo_Click(object sender, EventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				int id = 0;
				string saveDir = "Documents\\";
				string appPath = Request.PhysicalApplicationPath;
				if (FileUpload1.HasFile)
				{
					id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
					odbusuario = new BaseDatos();
					string nombre = odbusuario.verificarNombreArchivo(id, txtNombreArchivo.Text);
					if (string.IsNullOrEmpty(nombre))
					{
						string savePath = appPath + saveDir + txtNombreArchivo.Text + Convert.ToString(id) + ".pdf";
						odbusuario = new BaseDatos();
						odbusuario.gestionarArchivosSolicitudes(null, txtNombreArchivo.Text, txtNombreArchivo.Text + Convert.ToString(id) + ".pdf", id, Convert.ToString(Session["id"]), 1);
						FileUpload1.SaveAs(savePath);
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Documento adjuntado correctamente');", addScriptTags: true);
						odbusuario = new BaseDatos();
						DataSet ds = odbusuario.listaArchivosSolicitudes(id);
						gridArchivo.DataSource = ds.Tables[0];
						gridArchivo.DataBind();
						div3.Visible = true;
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('nombre del documento ya existe');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo adjuntar documento');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnRegistrarArchivo_Click", "adjuntar documento");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo adjuntar el documento');", addScriptTags: true);
		}
		}

		protected void btnEliminarArchivo_Click(object sender, EventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				int id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
				int idArchivo = Convert.ToInt32(Convert.ToString(Session["idArchivo"]));
				string saveDir = "Documents\\";
				string appPath = Request.PhysicalApplicationPath;
				string savePath = appPath + saveDir + txtNombreArchivo.Text + Convert.ToString(Session["idArchivoDoc"]) + ".pdf";
				odbusuario = new BaseDatos();
				if (odbusuario.gestionarArchivosSolicitudes(idArchivo, "", "", 0, "", 3))
				{
					File.Delete(savePath);
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Documento Eliminado');", addScriptTags: true);
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.listaArchivosSolicitudes(id);
					gridArchivo.DataSource = ds.Tables[0];
					gridArchivo.DataBind();
					div3.Visible = true;
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar documento');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnEliminarArchivo_Click", "eliminar documento adjunto");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar el documento');", addScriptTags: true);
		}
		}

		protected void btnSiguienteEtapa_Click(object sender, EventArgs e)
		{
			try { mpeEtapas.Show(); }
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnSiguienteEtapa_Click", "abrir modal etapas");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
			}
		}

		protected void Etapa_Command(object sender, CommandEventArgs e)
		{
			try
			{
				int idComand = Convert.ToInt32((string)e.CommandArgument);
				if (idComand != 0)
				{
					Session["etapaDocs"] = idComand;
					Session["prosesoDocs"] = 1;
					mpeComfirmarProceso.Show();
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar etapa');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.Etapa_Command", "asignar etapa");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar Etapa');", addScriptTags: true);
		}
		}

		protected void Estatus_Command(object sender, CommandEventArgs e)
		{
			try
			{
				int idComand = Convert.ToInt32((string)e.CommandArgument);
				if (idComand != 0)
				{
					Session["estatusDocs"] = idComand;
					Session["prosesoDocs"] = 2;
					mpeComfirmarProceso.Show();
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar estatus');", addScriptTags: true);
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.Estatus_Command", "asignar estatus");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar estatus');", addScriptTags: true);
		}
		}

		protected void btnConfirmarPorceso_Click(object sender, EventArgs e)
		{
			try
			{
				switch (Convert.ToInt32(Session["prosesoDocs"]))
				{
				case 1:
					int etapa = Convert.ToInt32(Session["etapaDocs"]);
					odbusuario = new BaseDatos();
					if (odbusuario.siguienteEtapa(Convert.ToInt32(Session["idDoc"]), etapa, 4, txtComentario.Text, Convert.ToString(Session["id"])))
					{
						btnFiltrar_Click(null, null);
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Etapa asignada');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar etapa');", addScriptTags: true);
					}
					break;
				case 2:
					int estatus = Convert.ToInt32(Session["estatusDocs"]);
					int etapaA = Convert.ToInt32(Session["etapaActual"]);
					odbusuario = new BaseDatos();
					if (odbusuario.asignarEstatus(Convert.ToInt32(Session["idDoc"]), etapaA, estatus, txtComentario.Text, Convert.ToString(Session["id"])))
					{
						btnFiltrar_Click(null, null);
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Estatus asignado');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar estatus');", addScriptTags: true);
					}
					break;
				default:
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar proceso');", addScriptTags: true);
					break;
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnConfirmarPorceso_Click", "confirmar etapa/estatus");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo realizar proceso');", addScriptTags: true);
		}
		}

		protected void btnEstatusEspeciales_Click(object sender, EventArgs e)
		{
			try { mpeEstatusEspeciales.Show(); }
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnEstatusEspeciales_Click", "abrir modal estatus especiales");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
			}
		}

		protected void limpiarModalClick(object sender, EventArgs e)
		{
			limpiarCampos();
			limpiarSessions();
		}

		protected void limpiarCampos() { }

		protected void limpiarCamposContenido()
		{
			txtCantidad.Text = string.Empty;
			txtCosto.Text = string.Empty;
			txtRenglon.Text = string.Empty;
			txtDecsContent.Text = string.Empty;
			txtUnidadContent.Text = string.Empty;
		}

		protected void limpiarSessions() { }

		protected void btnVerDoc_Click(object sender, EventArgs e)
		{
			string eleccion = "File_Compras_" + Convert.ToString(Session["idDoc"]);
			DataSet ds = new DataSet();
			odbusuario = new BaseDatos();
			ds = odbusuario.reporteBody(Convert.ToInt32(Convert.ToString(Session["idDoc"])));
			// ===== INICIO: Código Telerik Spreadsheet original (comentado - no eliminar) =====
			/*
			reporteCompra = new ReporteCompras();
			Workbook wk = reporteCompra.createexcellficha(ds, Convert.ToString(ds.Tables[1].Rows[0]["diradmin"]).ToUpper(), Convert.ToString(ds.Tables[1].Rows[0]["iddirectores"]).ToUpper(), Convert.ToString(ds.Tables[1].Rows[0]["Unidad"]).ToUpper());
			IWorkbookFormatProvider XlsformatProvider = new XlsxFormatProvider();
			byte[] renderedBytes = null;
			PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();
			using (MemoryStream ms = new MemoryStream())
			{
				pdfFormatProvider.Export(wk, ms);
				renderedBytes = ms.ToArray();
			}
			File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion + ".pdf", renderedBytes);
			string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
			embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
			embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
			embed += "</object>";
			ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion + ".pdf"));
			*/
			// ===== FIN: Código Telerik Spreadsheet original =====
			// ===== INICIO: Nuevo flujo con plantilla Word (.docx) =====
			string htmlPath = HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion + ".html";
			odbusuario = new BaseDatos();
			string templateRuta = odbusuario.obtenerPlantillaActiva();
			string templatePath;
			if (!string.IsNullOrEmpty(templateRuta))
				templatePath = HttpContext.Current.Server.MapPath(templateRuta);
			else
			{
				templatePath = HttpContext.Current.Server.MapPath("~/DocumentsPlantillas/plantilla_default.docx");
				if (!File.Exists(templatePath))
				{
					ReporteComprasDocx generador = new ReporteComprasDocx();
					generador.CrearPlantillaDefault(templatePath, HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png"), HttpContext.Current.Server.MapPath("~/userscontent/img/header.png"), HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png"));
				}
			}
			ReporteComprasDocx reporteDocx = new ReporteComprasDocx();
			reporteDocx.GenerarDocumentoDesdePlantilla(ds, templatePath, htmlPath);
			string embedHtml = "<iframe src=\"{0}\" width=\"1294px\" height=\"682px\" frameborder=\"0\"></iframe>";
			ltEmbed.Text = string.Format(embedHtml, ResolveUrl("~/DocumentsSolicitudes/" + eleccion + ".html") + "?v=" + DateTime.Now.Ticks);
			// ===== FIN: Nuevo flujo con plantilla Word =====
			MultiView1.ActiveViewIndex = 2;
			btnCloseContent.CommandArgument = "2";
		}

		protected void lbActualizarProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				int id = Convert.ToInt32(Session["idDoc"]);
				if ((Convert.ToInt32(dlistProveedor.SelectedIndex) != 1) & (Convert.ToInt32(dlistProveedor.SelectedIndex) != 0))
				{
					float valorConvertido3;
					float? precio = ((!string.IsNullOrWhiteSpace(txtPrecioProveedor.Text) && float.TryParse(txtPrecioProveedor.Text, out valorConvertido3)) ? new float?(valorConvertido3) : ((float?)null));
					odbusuario = new BaseDatos();
					if (odbusuario.gestionarProveedorSolicitudes(id, Convert.ToInt32(dlistProveedor.SelectedValue), precio, txtResolucion.Text, txtOrdenCompra.Text))
					{
						Session["proveedorActual"] = Convert.ToInt32(dlistProveedor.SelectedValue);
						Session["precioActual"] = precio;
						Session["oCompraActual"] = txtOrdenCompra.Text;
						btnActualizar_Click(null, null);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo actualizar los datos de proveedor');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar los datos del proveedor');", addScriptTags: true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.lbActualizarProveedor_Click", "actualizar proveedor en solicitud");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void lbCrearProveedor_Click(object sender, EventArgs e)
		{
			try { mpeCrearProveedor.Show(); }
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.lbCrearProveedor_Click", "abrir modal crear proveedor");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void btnCrearProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(txtNombreProveedorCrear.Text) & !string.IsNullOrEmpty(txtCorreoProveedorCrear.Text) & !string.IsNullOrEmpty(txtTelefonoProveedorCrear.Text))
				{
					odbusuario = new BaseDatos();
					if (odbusuario.gestionarProveedores(null, txtNombreProveedorCrear.Text, txtCorreoProveedorCrear.Text, txtTelefonoProveedorCrear.Text, 1))
					{
						odbusuario = new BaseDatos();
						DataSet ds = odbusuario.dLiataProveedores();
						dlistProveedor.Items.Clear();
						dlistProveedor.DataSource = ds;
						dlistProveedor.DataBind();
						dlistProveedor.Items.Insert(0, "\ud83d\udcac Seleccionar");
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Proveedor creado satisfactoriamente');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo crear el proveedor');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnCrearProveedor_Click", "crear proveedor");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void btnConfirmarCotizador_Click(object sender, EventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				if (dlistCotizadores.SelectedIndex > 0)
				{
					if (odbusuario.gestionarCotizadorSolicitudes(Convert.ToInt32(Session["idDoc"]), dlistCotizadores.SelectedValue, 1))
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Cotizador asignado');", addScriptTags: true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo asignar cotizador');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('escoja un cotizador');", addScriptTags: true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnConfirmarCotizador_Click", "confirmar cotizador");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void dlistUnidad_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int eleccion = 0;
				odbusuario = new BaseDatos();
				if (dlistUnidad.SelectedValue != "\ud83d\udccd Seleccionar")
				{
					eleccion = Convert.ToInt32(dlistUnidad.SelectedValue);
				}
				if (eleccion > 0)
				{
					dlistUsuario.Items.Clear();
					DataSet ds = odbusuario.filtroUsuarios("", "", "", null, Convert.ToInt32(dlistUnidad.SelectedItem.Value), null);
					dlistUsuario.Enabled = true;
					dlistUsuario.DataSource = ds;
					dlistUsuario.DataBind();
					dlistUsuario.Items.Insert(0, "\ud83d\udc68\ud83c\udffb\u200d\ud83d\udcbc Seleccionar");
				}
				else
				{
					dlistUsuario.Items.Clear();
					dlistUsuario.Enabled = false;
				}
			}
			catch (Exception ex) { ErrorHandler.LogError(ex, "busquedaDocumentos.dlistUnidad_SelectedIndexChanged", "filtrar usuarios por unidad"); }
		}

		protected void LbReplicaResolucion_Click(object sender, EventArgs e)
		{
			try
			{
				msjReplica.Text = "Crear copia para resoluci\u00f3n";
				idReplicaresolucion.Text = "ID# " + Convert.ToString(Session["idDoc"]) + "?";
				odbusuario = new BaseDatos();
				string catidad = odbusuario.cantidadReplicas(Convert.ToInt32(Session["idDoc"]), 1);
				lblCantidad.Text = "Cantidad de copias para resoluci\u00f3n = " + catidad;
				mpeReplicaResolucion.Show();
				Session["tipoReplica"] = 1;
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.LbReplicaResolucion_Click", "abrir modal replica resolución");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void LbReplicaSB_Click(object sender, EventArgs e)
		{
			try
			{
				msjReplica.Text = "Crear copia de solicitud de bienes";
				idReplicaresolucion.Text = "ID# " + Convert.ToString(Session["idDoc"]) + "?";
				odbusuario = new BaseDatos();
				string catidad = odbusuario.cantidadReplicas(Convert.ToInt32(Session["idDoc"]), 2);
				lblCantidad.Text = "Cantidad de copias para SB = " + catidad;
				mpeReplicaResolucion.Show();
				Session["tipoReplica"] = 2;
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.LbReplicaSB_Click", "abrir modal replica SB");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void btnConfirmarReplica_Click1(object sender, EventArgs e)
		{
			try
			{
				int tipo = Convert.ToInt32(Session["tipoReplica"]);
				switch (tipo)
				{
				case 1:
					odbusuario = new BaseDatos();
					string catidad2 = odbusuario.crearReplicaSolicitud(Convert.ToInt32(Session["idDoc"]), Convert.ToString(Session["id"]), tipo);
					msjReplicaResultado.Text = "Se ha creado la copia para resoluci\u00f3n";
					lblResultadoReplica.Text = "#" + catidad2;
					mpeResultadoReplica.Show();
					break;
				case 2:
					odbusuario = new BaseDatos();
					string catidad = odbusuario.crearReplicaSolicitud(Convert.ToInt32(Session["idDoc"]), Convert.ToString(Session["id"]), tipo);
					msjReplicaResultado.Text = "Se ha creado la copia para SB";
					lblResultadoReplica.Text = "#" + catidad;
					mpeResultadoReplica.Show();
					break;
				default:
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo replicar');", addScriptTags: true);
					break;
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnConfirmarReplica_Click1", "replicar solicitud");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo replicar');", addScriptTags: true);
		}
		}

		protected void LbTransferRenglones_Click(object sender, EventArgs e)
		{
			try
			{
				odbusuario = new BaseDatos();
				string catidadText = odbusuario.cantidadReplicas(Convert.ToInt32(Session["idDoc"]), 2);
				int catidad = Convert.ToInt32(catidadText);
				if (catidad > 0)
				{
					int eleccion = Convert.ToInt32(Session["idDoc"]);
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.listaDetallesSolicitudes(Convert.ToInt32(eleccion));
					gridContenidoTransferir.DataSource = ds;
					gridContenidoTransferir.DataBind();
					mpeTransferir.Show();
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No cuenta con copias para transferir');", addScriptTags: true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.LbTransferRenglones_Click", "abrir modal transferir renglones");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}

		protected void gridContenidoTransferir_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName.Equals("transferirRenglon", StringComparison.CurrentCultureIgnoreCase))
			{
				try
				{
					GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
					string eleccion = gvr.Cells[0].Text.ToString().Trim();
					Session["idRenglonTrasnfer"] = eleccion;
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.dListaReplicasSB(Convert.ToInt32(Session["idDoc"]));
					dlistReplicaSB.Items.Clear();
					dlistReplicaSB.DataSource = ds;
					dlistReplicaSB.DataBind();
					dlistReplicaSB.Items.Insert(0, "\u2197\ufe0f Seleccionar");
					mpeConfirmarTransferir.Show();
				}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.gridContenidoTransferir_RowCommand", "transferir renglón");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
			}
		}

		protected void btnTransferirReplica_Click(object sender, EventArgs e)
		{
			try
			{
				if (dlistReplicaSB.SelectedIndex > 0)
				{
					odbusuario = new BaseDatos();
					string catidadText = odbusuario.cantidadReplicas(Convert.ToInt32(Session["idDoc"]), 2);
					int catidad = Convert.ToInt32(catidadText);
					int id = Convert.ToInt32(Convert.ToString(Session["idRenglonTrasnfer"]));
					int idTransfer = Convert.ToInt32(dlistReplicaSB.SelectedValue);
					if (catidad > 0)
					{
						int eleccion = Convert.ToInt32(Session["idDoc"]);
						odbusuario = new BaseDatos();
						if (odbusuario.TransferirSB(id, idTransfer))
						{
							ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Transferencia a " + idTransfer + " completa');", addScriptTags: true);
							odbusuario = new BaseDatos();
							DataSet ds = odbusuario.listaDetallesSolicitudes(eleccion);
							gridContenido.DataSource = ds;
							gridContenido.DataBind();
						}
						else
						{
							ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo transferir');", addScriptTags: true);
						}
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No cuenta con copias para transferir');", addScriptTags: true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Seleccionar a cual transferir');", addScriptTags: true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.LogError(ex, "busquedaDocumentos.btnTransferirReplica_Click", "confirmar transferencia");
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
			}
		}


		protected void btnPanelMensajes_Click(object sender, CommandEventArgs e)
		{
			try
			{
				if (Convert.ToInt32((string)e.CommandArgument) == 1)
				{
					chatArea.Enabled = true;
					odbusuario = new BaseDatos();
					DataSet ds = odbusuario.listaComentariosSolicitudes(Convert.ToInt32(Session["idDoc"]));
					imgEncendido.ImageUrl = "~/userscontent/icon/bot\u00f3n-de-apagado.png";
					lbtnContorlMensajes.CommandArgument = "2";
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						crearMSJBox(dr["idUsuaruios"].ToString(), dr["nombre"].ToString(), dr["fecha"].ToString(), dr["comentario"].ToString(), dr["descripciones"].ToString(), Convert.ToInt32(dr["tipos"].ToString()));
					}
					uPanelChat.Update();
					linkBMsj.Enabled = true;
				}
				else
				{
					imgEncendido.ImageUrl = "~/userscontent/icon/bot\u00f3n-de-encendido.png";
					lbtnContorlMensajes.CommandArgument = "1";
					chatArea.Enabled = false;
					linkBMsj.Enabled = false;
					txtMsj.Text = "";
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.btnPanelMensajes_Click", "cargar comentarios");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo desplegar correctamente los comentarios');", addScriptTags: true);
		}
		}

		private void crearMSJBox(string users, string userN, string dates, string msjs, string Etapa, int tipo)
		{
			string User = Convert.ToString(Session["id"]);
			Panel panelMsjArea = new Panel();
			Panel panelMsjData = new Panel();
			panelMsjArea.CssClass = "clearfix align-bottom mb-5";
			Label labelUser = new Label();
			labelUser.Text = userN + " - ";
			labelUser.CssClass = "message-data-time";
			Label labelDate = new Label();
			labelDate.Text = dates;
			labelDate.CssClass = "message-data-time";
			Label labelMsjContent = new Label();
			labelMsjContent.Text = msjs;
			if (User == users)
			{
				if (tipo == 1) { panelMsjData.CssClass = "message-data text-right"; labelMsjContent.CssClass = "message other-message float-right col-lg-7 col-md-9"; }
				else { panelMsjData.CssClass = "message-data text-right"; labelMsjContent.CssClass = "message other-message bg-danger text-white float-right col-lg-7 col-md-9"; }
			}
			else
			{
				if (tipo == 1) { panelMsjData.CssClass = "message-data"; labelMsjContent.CssClass = "message my-message col-lg-7 col-md-9"; }
				else { panelMsjData.CssClass = "message-data"; labelMsjContent.CssClass = "message my-message text-white bg-danger col-lg-7 col-md-9"; }
			}
			panelMsjData.Controls.Add(labelUser);
			panelMsjData.Controls.Add(labelDate);
			panelMsjArea.Controls.Add(panelMsjData);
			panelMsjArea.Controls.Add(labelMsjContent);
			panelChat.Controls.Add(panelMsjArea);
		}

		protected void linkBMsj_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(txtMsj.Text))
				{
					int id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
					odbusuario = new BaseDatos();
					string[] datos = odbusuario.datosSolicitudes(id);
					odbusuario = new BaseDatos();
					if (odbusuario.gestionarComentariuosSolicitudes(null, txtMsj.Text, id, Convert.ToInt32(datos[7]), Convert.ToInt32(datos[7]), Convert.ToString(Session["id"]), 1, 1))
					{
						CommandEventArgs commandEventArgs = new CommandEventArgs(null, "1");
						btnPanelMensajes_Click(null, commandEventArgs);
						txtMsj.Text = string.Empty;
						uPanelChat.Update();
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar mensaje');", addScriptTags: true);
						uPanelChat.Update();
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('El mensaje no puede estar vac\u00edo');", addScriptTags: true);
					txtMsj.Text = string.Empty;
					uPanelChat.Update();
				}
			}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "busquedaDocumentos.linkBMsj_Click", "registrar comentario");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo registrar comentario');", addScriptTags: true);
		}
		}
	}
}
