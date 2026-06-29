namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Data;
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

    public partial class PorRecibir : Page
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

    	protected ModalPopupExtender mpeConfirmarAtencion;

    	protected HiddenField HiddenField14;

    	protected ModalPopupExtender mpeRechazarAtencion;

    	protected Panel PanleRechazarAtencion;

    	protected Button closeRechazarAtencion;

    	protected Panel panel9;

    	protected Label Label38;

    	protected Label lblRechazarAtencionReg;

    	protected Button btnRechazarAtencion;

    	protected Panel panelConfirmar;

    	protected Button closeConfirmarAtencion;

    	protected Panel panel8;

    	protected Label Label43;

    	protected Label lblConfirmarAtencionReg;

    	protected Button btnConfirmarAtencion;

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

    	protected Panel Panel3;

    	protected Label Label9;

    	protected TextBox txtId;

    	protected Label Label44;

    	protected TextBox txtSolicitudBien;

    	protected Label Label45;

    	protected TextBox txtTituloF;

    	protected HtmlGenericControl div8;

    	protected Label Label49;

    	protected DropDownList dlistEstatus;

    	protected HtmlGenericControl groupUnidadFilt;

    	protected Label Label51;

    	protected DropDownList dlistUnidad;

    	protected HtmlGenericControl groupEtapaFilt;

    	protected Label Label48;

    	protected DropDownList dListEtapa;

    	protected HtmlGenericControl divProCot;

    	protected Label Label46;

    	protected DropDownList dlistCotizador;

    	protected Label Label50;

    	protected DropDownList dlistProveedorF;

    	protected Button btnFiltrar;

    	protected GridView GridView1;

    	protected View viewDescripciones;

    	protected Panel panelInfoGeneral;

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

    	protected Button btnVerDoc1;

    	protected Button btnEnviar;

    	protected Panel panelBtnsOthers;

    	protected Button btnSiguienteEtapa;

    	protected Button btnVerDoc;

    	protected Button btnAsignar;

    	protected Button btnEstatusEspeciales;

    	protected Label headNameCotizador;
    	protected Panel panelInfoPreveedor;
    	protected HtmlGenericControl divProveedores3;
    	protected Label lblProv1;
    	protected Label lblPrecio1;
    	protected Label lblRes3_1;
    	protected Label lblOC3_1;
    	protected Label lblProv2;
    	protected Label lblPrecio2;
    	protected Label lblRes3_2;
    	protected Label lblOC3_2;
    	protected Label lblProv3;
    	protected Label lblPrecio3;
    	protected Label lblRes3_3;
    	protected Label lblOC3_3;

    	protected HtmlGenericControl div2;

    	protected Label Label7;

    	protected Label lblProveedor;

    	protected Label Label24;

    	protected Label lblPrecioProveedor;

    	protected Label Label25;

    	protected Label lblResolucion;

    	protected Label Label26;

    	protected Label lblOrdenCompra;

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

    	protected Image Image1;

    	protected LinkButton lbCrearProveedor;

    	protected Image Image3;

    	protected Panel Panel1;

    	protected HtmlGenericControl div3;

    	protected GridView gridContenido;

    	protected Panel Panel2;

    	protected Button btnAddREg;

    	protected HtmlGenericControl div5;

    	protected GridView gridArchivo;

    	protected Panel panelMensajes;

    	protected UpdatePanel uPanelChat;

    	protected LinkButton lbtnContorlMensajes;

    	protected Image imgEncendido;

    	protected HtmlGenericControl div6;

    	protected Panel chatArea;

    	protected Panel ChatHistory;

    	protected Panel panelChat;

    	protected TextBox txtMsj;

    	protected LinkButton linkBMsj;

    	protected Image Image2;

    	protected View viewPDF;

    	protected Button btnCloseContent;

    	protected Literal ltEmbed;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if (!Page.IsPostBack)
    		{
    			if ((string)Session["type"] == "4")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterRecepcionCompras.master";
    			}
    			else if ((string)Session["type"] == "6")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterOtrasRecepciones.master";
    			}
    		}
    	}

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
    		else if ((Convert.ToInt32(Session["type"]) == 4) | (Convert.ToInt32(Session["type"]) == 6))
    		{
    			odbusuario = new BaseDatos();
    			DataSet dsinfoU = new DataSet();
    			dsinfoU = odbusuario.infoUsuarios(Convert.ToString(Session["id"]));
    			Session["Recepcion"] = Convert.ToString(dsinfoU.Tables[0].Rows[0]["unidad"]);
    			DataSet ds = new DataSet();
    			ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    			if (Convert.ToInt32(Session["unidad"]) == 2)
    			{
    				int columnaFSIndex = GetColumnIndexByName(GridView1, "F-Sol");
    				int columnaOCompraIndex = GetColumnIndexByName(GridView1, "Orden de compra");
    				if (columnaFSIndex != -1)
    				{
    					GridView1.Columns[columnaFSIndex].Visible = false;
    				}
    				if (columnaOCompraIndex != -1)
    				{
    					GridView1.Columns[columnaOCompraIndex].Visible = true;
    				}
    			}
    			GridView1.DataSource = ds;
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
    			dlistCotizadores.Items.Insert(0, "\ud83d\udc68\ud83c\udffb\u200d\ud83d\udcbc Seleccionar");
    			dlistCotizador.Items.Clear();
    			dlistCotizador.DataSource = ds3;
    			dlistCotizador.DataBind();
    			dlistCotizador.Items.Insert(0, "\ud83d\udc77\u200d♂\ufe0f Seleccionar");
    			dlistProveedorF.Items.Clear();
    			dlistProveedorF.DataSource = ds2;
    			dlistProveedorF.DataBind();
    			dlistProveedorF.Items.Insert(0, "\ud83e\udd35\u200d♂\ufe0f Seleccionar");
    			odbusuario = new BaseDatos();
    			DataSet ds4 = odbusuario.dListaEtapaSolicitudes();
    			dListEtapa.Items.Clear();
    			dListEtapa.DataSource = ds4;
    			dListEtapa.DataBind();
    			dListEtapa.Items.Insert(0, "\ud83d\udd00 Seleccionar");
    			odbusuario = new BaseDatos();
    			DataSet ds5 = odbusuario.dListaUnidadesSolicitantes();
    			dlistUnidad.Items.Clear();
    			dlistUnidad.DataSource = ds5;
    			dlistUnidad.DataBind();
    			dlistUnidad.Items.Insert(0, "\ud83d\udccd Seleccionar");
    			odbusuario = new BaseDatos();
    			DataSet ds6 = odbusuario.dListaEstatusSolicitudes();
    			dlistEstatus.Items.Clear();
    			dlistEstatus.DataSource = ds6;
    			dlistEstatus.DataBind();
    			dlistEstatus.Items.Insert(0, "⚙\ufe0f Seleccionar");
    			if ((Convert.ToInt32(Session["type"]) == 2) | (Convert.ToInt32(Session["type"]) == 3))
    			{
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelBtnsOthers.Enabled = false;
    				panelBtnsOthers.Visible = false;
    			}
    			if (Convert.ToInt32(Session["type"]) == 4)
    			{
    				panelBtnsOthers.Enabled = false;
    				panelBtnsOthers.Visible = false;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelInfoGeneral1.Enabled = false;
    				panelInfoGeneral3_1.Enabled = false;
    				panelInfoGeneral3_2.Enabled = false;
    				txtFecha.Enabled = false;
    			}
    			if (Convert.ToInt32(Session["type"]) == 6)
    			{
    				panelBtnsOthers.Enabled = false;
    				panelBtnsOthers.Visible = false;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelInfoGeneral.Enabled = false;
    				btnActualizar.Enabled = false;
    				btnActualizar.Visible = false;
    			}
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
    				bool has3Prov = !string.IsNullOrEmpty(datos[15]) && datos[15] != "0";
    				if (has3Prov)
    				{
    					divProveedores3.Visible = true;
    					div2.Visible = false;

    					BaseDatos dbProv = new BaseDatos();
    					string[] p1 = dbProv.datosProveedores(Convert.ToInt32(datos[15]));
    					lblProv1.Text = p1 != null ? p1[1] : "SIN ASIGNAR";
    					lblPrecio1.Text = datos[18];
    					lblRes3_1.Text = datos[2];
    					lblOC3_1.Text = datos[12];

    					dbProv = new BaseDatos();
    					string[] p2 = dbProv.datosProveedores(Convert.ToInt32(datos[16]));
    					lblProv2.Text = p2 != null ? p2[1] : "SIN ASIGNAR";
    					lblPrecio2.Text = datos[19];
    					lblRes3_2.Text = datos[2];
    					lblOC3_2.Text = datos[12];

    					dbProv = new BaseDatos();
    					string[] p3 = dbProv.datosProveedores(Convert.ToInt32(datos[17]));
    					lblProv3.Text = p3 != null ? p3[1] : "SIN ASIGNAR";
    					lblPrecio3.Text = datos[20];
    					lblRes3_3.Text = datos[2];
    					lblOC3_3.Text = datos[12];
    				}
    				else
    				{
    					divProveedores3.Visible = false;
    					div2.Visible = true;

    					BaseDatos dbProv = new BaseDatos();
    					string[] datosProveedor = dbProv.datosProveedores(Convert.ToInt32(datos[10]));
    					lblProveedor.Text = datosProveedor != null ? datosProveedor[1] : "SIN ASIGNAR";
    					lblPrecioProveedor.Text = datos[11];
    					lblResolucion.Text = datos[2];
    					lblOrdenCompra.Text = datos[12];
    				}

    				if (!string.IsNullOrEmpty(datos[13]))
    				{
    					odbusuario = new BaseDatos();
    					DataSet dsCotizador = odbusuario.infoUsuarios(datos[13]);
    					if (dsCotizador != null && dsCotizador.Tables.Count > 0 && dsCotizador.Tables[0].Rows.Count > 0)
    					{
    						headNameCotizador.Text = "Cotizador: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["nombres"]) + " " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["apellidos"]);
    						headNameCotizador.ToolTip = "Telefono: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["telefono"]) + "\nEmail: " + Convert.ToString(dsCotizador.Tables[0].Rows[0]["email"]);
    					}
    					else
    					{
    						headNameCotizador.Text = "Cotizador: NO ENCONTRADO";
    						headNameCotizador.ToolTip = "";
    					}
    				}
    				else
    				{
    					headNameCotizador.Text = "Cotizador: NO ASIGNADO";
    					headNameCotizador.ToolTip = "";
    				}

    				odbusuario = new BaseDatos();
    				string[] datosProveedor2 = odbusuario.datosProveedores(Convert.ToInt32(datos[10]));
    				if (datosProveedor2 != null && dlistProveedor.Items.Count > 0)
    				{
    					dlistProveedor.SelectedValue = datosProveedor2[0];
    				}
    				txtPrecioProveedor.Text = datos[11];
    				txtResolucion.Text = datos[2];
    				txtOrdenCompra.Text = datos[12];
    				odbusuario = new BaseDatos();
    				DataSet ds2 = new DataSet();
    				ds2 = odbusuario.listaDetallesSolicitudes(Convert.ToInt32(eleccion));
    				gridContenido.DataSource = ds2;
    				gridContenido.DataBind();
    				odbusuario = new BaseDatos();
    				DataSet ds3 = odbusuario.listaArchivosSolicitudes(Convert.ToInt32(eleccion));
    				gridArchivo.DataSource = ds3.Tables[0];
    				gridArchivo.DataBind();
    				CommandEventArgs commandEventArgs = new CommandEventArgs("editar", "1");
    				btnPanelMensajes_Click(null, commandEventArgs);
    			}
    			if (e.CommandName.Equals("cargarDoc", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion2 = gvr2.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion2;
    			}
    			if (e.CommandName.Equals("Asignar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr3 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion3 = gvr3.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion3;
    				mpeCotizadores.Show();
    			}
    			if (e.CommandName.Equals("Confirmar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr4 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion4 = gvr4.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion4;
    				lblConfirmarAtencionReg.Text = eleccion4;
    				mpeConfirmarAtencion.Show();
    				string[] datos2 = new string[14];
    				datos2 = odbusuario.datosSolicitudes(Convert.ToInt32(eleccion4));
    				Session["idDocEtapa"] = datos2[7];
    			}
    			if (e.CommandName.Equals("Rechazar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr5 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion5 = gvr5.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion5;
    				lblRechazarAtencionReg.Text = eleccion5;
    				mpeRechazarAtencion.Show();
    				string[] datos3 = new string[14];
    				datos3 = odbusuario.datosSolicitudes(Convert.ToInt32(eleccion5));
    				Session["idDocEtapa"] = datos3[7];
    			}
    			if (e.CommandName.Equals("Enviar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr6 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion6 = gvr6.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion6;
    				lblRechazarAtencionReg.Text = eleccion6;
    				btnSiguienteEtapa_Click(null, null);
    			}
    			if (e.CommandName.Equals("verDoc", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr7 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion7 = gvr7.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion7;
    				DataSet ds4 = new DataSet();
    				odbusuario = new BaseDatos();
    				ds4 = odbusuario.reporteBody(Convert.ToInt32(eleccion7));
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
    				string eleccion8 = "File_Compras_" + eleccion7;
    				File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion8 + ".pdf", renderedBytes);
    				string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
    				embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    				embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    				embed += "</object>";
    				ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion8 + ".pdf"));
    				MultiView1.ActiveViewIndex = 2;
    				btnCloseContent.CommandArgument = "1";
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.GridView1_RowCommand", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    	{
    		if (e.Row.RowType == DataControlRowType.DataRow)
    		{
    			Button btnAsignar = e.Row.FindControl("Button3") as Button;
    			Button btnConfirmar = e.Row.FindControl("Button4") as Button;
    			Button btnRechazar = e.Row.FindControl("Button5") as Button;
    			Button btnEnviar = e.Row.FindControl("Button6") as Button;
    			if (Convert.ToInt32(Session["type"]) == 4)
    			{
    				btnAsignar.Visible = false;
    			}
    			string estatus = e.Row.Cells[7].Text;
    			btnAsignar.Visible = (estatus != "Enviado") & (estatus != "Rechazado") & (Convert.ToInt32(Session["type"]) != 6);
    			btnConfirmar.Visible = estatus != "Recibido";
    			btnRechazar.Visible = (estatus != "Recibido") & (estatus != "Rechazado");
    			btnEnviar.Visible = (estatus != "Enviado") & (estatus != "Rechazado") & (Convert.ToInt32(Session["type"]) != 4);
    		}
    	}

    	protected void btnFiltrar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int? id = null;
    			int? estatus = null;
    			int? etapa = null;
    			int? unidad = null;
    			int? proveedor = null;
    			string solicitudBien = "";
    			string titulo = "";
    			string cotizador = "";
    			id = VerificarCampoIntNull(txtId);
    			estatus = VerificarDList(dlistEstatus);
    			etapa = VerificarDList(dListEtapa);
    			cotizador = VerificarDListText(dlistCotizador);
    			unidad = VerificarDList(dlistUnidad);
    			proveedor = VerificarDList(dlistProveedorF);
    			solicitudBien = VerificarCampo(txtSolicitudBien);
    			titulo = VerificarCampo(txtTituloF);
    			DataSet ds = filtros(id, solicitudBien, null, titulo, estatus, etapa, null, cotizador, unidad, null, proveedor, null, null);
    			GridView1.DataSource = ds;
    			GridView1.DataBind();
    			MultiView1.ActiveViewIndex = 0;
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnFiltrar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo aplicar filtro');", addScriptTags: true);
    		}
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
    			ErrorHandler.LogError(ex, "PorRecibir.VerificarDList", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "PorRecibir.VerificarDListText", "grid/button handler");
    			return "";
    		}
    	}

    	protected int? VerificarCampoIntNull(TextBox txt)
    	{
    		if (!string.IsNullOrEmpty(txt.Text))
    		{
    			return Convert.ToInt32(txt.Text);
    		}
    		return null;
    	}

    	protected string VerificarCampo(TextBox txt)
    	{
    		if (!string.IsNullOrEmpty(txt.Text))
    		{
    			return txt.Text;
    		}
    		return string.Empty;
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
    				odbusuario = new BaseDatos();
    				DataSet ds = new DataSet();
    				ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    				GridView1.DataSource = ds;
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Solicitud eliminada');", addScriptTags: true);
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo eliminar la solicitud');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnEliminarSolicitud_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar la solicitud');", addScriptTags: true);
    		}
    	}

    	protected DataSet filtros(int? id, string solicitudBien, string resolucion, string descripciones, int? estatus, int? etapa, string usuaruio, string cotizador, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		if (Session["id"] != null && Session["type"] != null)
    		{
    			PeriodoHelper.ClampFechasBusqueda(Session["id"].ToString(), Session["type"].ToString(), ref desde, ref hasta);
    		}

    		DataSet ds = new DataSet();
    		odbusuario = new BaseDatos();
    		int userRoleF = Convert.ToInt32(Session["rolesF"]);
    		int filterEstatus = estatus ?? 4; // Por defecto sólo mostrar pendientes ("Enviado")
    		
    		string cotizadorFilter = cotizador;
    		if (userRoleF == 9)
    		{
    			cotizadorFilter = Convert.ToString(Session["id"]);
    		}
    		
    		ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, filterEstatus, userRoleF, usuaruio, cotizadorFilter, unidad, director, proveedor, desde, hasta);

    		if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    		{
    			string currentUserId = Convert.ToString(Session["id"]);
    			DataTable dt = ds.Tables[0];
    			for (int i = dt.Rows.Count - 1; i >= 0; i--)
    			{
    				DataRow row = dt.Rows[i];
    				int solId = Convert.ToInt32(row["#"]);
    				BaseDatos dbComents = new BaseDatos();
    				DataSet dsComents = dbComents.listaComentariosSolicitudes(solId);
    				if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
    				{
    					string lastUserComment = Convert.ToString(dsComents.Tables[0].Rows[0]["idUsuaruios"]);
    					if (lastUserComment.Equals(currentUserId, StringComparison.OrdinalIgnoreCase))
    					{
    						dt.Rows.RemoveAt(i);
    					}
    				}
    			}
    		}
    		return ds;
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
    			if ((Convert.ToInt32(Session["type"]) == 4) | (Convert.ToInt32(Session["type"]) == 5))
    			{
    				if ((!string.IsNullOrEmpty(txtUnidad.Text) & !string.IsNullOrEmpty(txtTitulo.Text) & !string.IsNullOrEmpty(txtSolicitante.Text) & !string.IsNullOrEmpty(txtFecha.Text) & !string.IsNullOrEmpty(txtDescripcion.Text)) && checkeditemscount > 0)
    				{
    					id = Convert.ToInt32(Session["idDoc"]);
    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarSolicitudes(id, Convert.ToString(Session["resolucionActual"]), txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(Session["estatusActual"]), Convert.ToInt32(Session["etapaActual"]), Convert.ToString(Session["UsuarioActual"]), proveedorActual, txtSB.Text, precioActual, Convert.ToString(Session["oCompraActual"]), 2))
    					{
    						btnActualizar.Visible = e != null;
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
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo actualizar la el registro');", addScriptTags: true);
    					}
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos de el area de generales');", addScriptTags: true);
    				}
    			}
    			else if ((Convert.ToInt32(Session["type"]) == 2) | (Convert.ToInt32(Session["type"]) == 3))
    			{
    				if ((!string.IsNullOrEmpty(txtUnidad.Text) & !string.IsNullOrEmpty(txtTitulo.Text) & !string.IsNullOrEmpty(txtSolicitante.Text) & !string.IsNullOrEmpty(txtFecha.Text) & !string.IsNullOrEmpty(txtDescripcion.Text)) && checkeditemscount > 0)
    				{
    					id = Convert.ToInt32(Session["idDoc"]);
    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarSolicitudes(id, Convert.ToString(Session["resolucionActual"]), txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(Session["estatusActual"]), Convert.ToInt32(Session["etapaActual"]), Convert.ToString(Session["UsuarioActual"]), proveedorActual, txtSB.Text, precioActual, Convert.ToString(Session["oCompraActual"]), 2))
    					{
    						odbusuario = new BaseDatos();
    						foreach (ListItem lista2 in checkTipoSol.Items)
    						{
    							if (lista2.Selected)
    							{
    								odbusuario.gestionarTipoSolucitudesSolicitud(id, Convert.ToInt32(lista2.Value), 1);
    							}
    							else
    							{
    								odbusuario.gestionarTipoSolucitudesSolicitud(id, Convert.ToInt32(lista2.Value), 3);
    							}
    						}
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Generales de registro actualizado');", addScriptTags: true);
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo actualizar la el registro');", addScriptTags: true);
    					}
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos de el area de generales');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Usuario no autorizado para actualizar informacion del registro');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnActualizar_Click", "grid/button handler");
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
    			catch (Exception ex)
    			{
    				ErrorHandler.LogError(ex, "PorRecibir.gridContenido_RowCommand", "grid/button handler");
    			}
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
    			catch (Exception ex)
    			{
    				ErrorHandler.LogError(ex, "PorRecibir.gridContenido_RowCommand", "grid/button handler");
    			}
    		}
    	}

    	protected void btnEliminarRenglon_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = 0;
    			int idDoc = 0;
    			odbusuario = new BaseDatos();
    			id = Convert.ToInt32(Session["idRenglon"]);
    			idDoc = Convert.ToInt32(Session["idDoc"]);
    			if (odbusuario.gestionarDetallesSolicitudes(id, "", 0, "", "", "", "", "", 0, 3))
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds = new DataSet();
    				ds = odbusuario.listaDetallesSolicitudes(idDoc);
    				gridContenido.DataSource = ds;
    				gridContenido.DataBind();
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Renglon eliminado');", addScriptTags: true);
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo salvar la el renglon');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnEliminarRenglon_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar renglon');", addScriptTags: true);
    		}
    	}

    	protected void btnActualizarRenglon_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if (!string.IsNullOrEmpty(txtDecsContentEdit.Text))
    			{
    				int id = 0;
    				int idDoc = 0;
    				odbusuario = new BaseDatos();
    				id = Convert.ToInt32(Session["idRenglon"]);
    				idDoc = Convert.ToInt32(Session["idDoc"]);
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
    					DataSet ds = new DataSet();
    					ds = odbusuario.listaDetallesSolicitudes(idDoc);
    					gridContenido.DataSource = ds;
    					gridContenido.DataBind();
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Renglon editado');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo salvar la el renglon');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnActualizarRenglon_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar renglon');", addScriptTags: true);
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
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.gridArchivo_RowCommand", "grid/button handler");
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

    	protected void gridArchivo_RowDataBound(object sender, GridViewRowEventArgs e)
    	{
    		if (e.Row.RowType == DataControlRowType.DataRow)
    		{
    			Button btneliminarArchivo = (Button)e.Row.FindControl("Button2");
    			if ((Convert.ToString(Session["type"]) == "6") | (Convert.ToString(Session["type"]) == "5") | (Convert.ToString(Session["type"]) == "4"))
    			{
    				btneliminarArchivo.Visible = false;
    			}
    			else
    			{
    				btneliminarArchivo.Visible = true;
    			}
    		}
    	}

    	protected void btnAddREg_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			mppeRegistrarArchivo.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnAddREg_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnRegistrarArchivo_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			int id = 0;
    			string[] datos = new string[8];
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
    			ErrorHandler.LogError(ex, "PorRecibir.btnRegistrarArchivo_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo adjuntar el documento');", addScriptTags: true);
    		}
    	}

    	protected void btnEliminarArchivo_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			int id = 0;
    			int idArchivo = 0;
    			string saveDir = "Documents\\";
    			string appPath = Request.PhysicalApplicationPath;
    			id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    			idArchivo = Convert.ToInt32(Convert.ToString(Session["idArchivo"]));
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
    			ErrorHandler.LogError(ex, "PorRecibir.btnEliminarArchivo_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo adjuntar el documento');", addScriptTags: true);
    		}
    	}

    	protected void btnSiguienteEtapa_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			mpeEtapas.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnSiguienteEtapa_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
    		}
    	}

    	protected void Etapa_Command(object sender, CommandEventArgs e)
    	{
    		try
    		{
    			int idComand = 0;
    			idComand = Convert.ToInt32((string)e.CommandArgument);
    			if (idComand != 0)
    			{
    				Session["etapaDocs"] = idComand;
    				Session["prosesoDocs"] = 1;
    				mpeComfirmarProceso.Show();
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar estatus');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.Etapa_Command", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar Etapa');", addScriptTags: true);
    		}
    	}

    	protected void Estatus_Command(object sender, CommandEventArgs e)
    	{
    		try
    		{
    			int idComand = 0;
    			idComand = Convert.ToInt32((string)e.CommandArgument);
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
    			ErrorHandler.LogError(ex, "PorRecibir.Estatus_Command", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar estatus');", addScriptTags: true);
    		}
    	}

    	protected void btnConfirmarPorceso_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int proceso = 0;
    			int etapa = 0;
    			int estatus = 0;
    			switch (Convert.ToInt32(Session["prosesoDocs"]))
    			{
    			case 1:
    				etapa = Convert.ToInt32(Session["etapaDocs"]);
    				odbusuario = new BaseDatos();
    				if (odbusuario.siguienteEtapa(Convert.ToInt32(Session["idDoc"]), etapa, 4, txtComentario.Text, Convert.ToString(Session["id"])))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds2 = new DataSet();
    					ds2 = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    					GridView1.DataSource = ds2;
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Etapa asignada');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar etapa');", addScriptTags: true);
    				}
    				break;
    			case 2:
    			{
    				estatus = Convert.ToInt32(Session["estatusDocs"]);
    				int etapaA = Convert.ToInt32(Session["etapaActual"]);
    				odbusuario = new BaseDatos();
    				if (odbusuario.asignarEstatus(Convert.ToInt32(Session["idDoc"]), etapaA, estatus, txtComentario.Text, Convert.ToString(Session["id"])))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds = new DataSet();
    					ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    					GridView1.DataSource = ds;
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Estatus asignado');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar estatus');", addScriptTags: true);
    				}
    				break;
    			}
    			default:
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar proceso');", addScriptTags: true);
    				break;
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnConfirmarPorceso_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo realizar proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnEstatusEspeciales_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			mpeEstatusEspeciales.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnEstatusEspeciales_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
    		}
    	}

    	protected void limpiarModalClick(object sender, EventArgs e)
    	{
    		limpiarCampos();
    		limpiarSessions();
    	}

    	protected void limpiarCampos()
    	{
    	}

    	protected void limpiarSessions()
    	{
    	}

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
    		string eleccion2 = eleccion;
    		File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion2 + ".pdf", renderedBytes);
    		string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
    		embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    		embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    		embed += "</object>";
    		ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion2 + ".pdf"));
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

    	protected void btnCloseContent_Click(object sender, EventArgs e)
    	{
    		MultiView1.ActiveViewIndex = 0;
    	}

    	private void crearBtnEstatusEspeciales(string text, string value)
    	{
    		try
    		{
    			Button btn = new Button();
    			btn.Attributes.Add("runat", "server");
    			btn.Text = text;
    			btn.ID = "btn_" + text;
    			btn.CssClass = "btn btn-dark form-control text-center";
    			btn.CommandArgument = value;
    			btn.CommandName = "Etapa_Command";
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.crearBtnEstatusEspeciales", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se generar lista');", addScriptTags: true);
    		}
    	}

    	protected void btnEnviar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if ((string)Session["type"] == "2")
    			{
    				int id = 0;
    				id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    				odbusuario = new BaseDatos();
    				if (odbusuario.asignarEstatus(id, 1, 2, "Enviado a Dir.Adm", Convert.ToString(Session["id"])))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Enviado');", addScriptTags: true);
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.listaArchivosSolicitudes(id);
    					gridArchivo.DataSource = ds.Tables[0];
    					gridArchivo.DataBind();
    					div3.Visible = true;
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo enviar');", addScriptTags: true);
    				}
    			}
    			else if ((string)Session["type"] == "3")
    			{
    				int id2 = 0;
    				id2 = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    				odbusuario = new BaseDatos();
    				if (odbusuario.siguienteEtapa(id2, 2, 4, "Enviado a Dir.Adm", Convert.ToString(Session["id"])))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Enviado a Dir. Adm');", addScriptTags: true);
    					odbusuario = new BaseDatos();
    					DataSet ds2 = odbusuario.listaArchivosSolicitudes(id2);
    					gridArchivo.DataSource = ds2.Tables[0];
    					gridArchivo.DataBind();
    					div3.Visible = true;
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo enviar');", addScriptTags: true);
    				}
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnEnviar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo enviar');", addScriptTags: true);
    		}
    	}

    	protected void btnAprobar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if ((string)Session["type"] == "3")
    			{
    				Session["etapaDocs"] = 2;
    				Session["prosesoDocs"] = 1;
    				mpeComfirmarProceso.Show();
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no esta autorizado para realizar este proseso');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnAprobar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se generar lista');", addScriptTags: true);
    		}
    	}

    	private void crearBtnCotizador(string id, string text, string value)
    	{
    		Button btn = new Button();
    		btn.ID = id;
    		btn.Text = text;
    		btn.CommandName = text;
    		btn.Attributes.Add("OnCommand", "cotizador_Command");
    		btn.CommandArgument = value;
    		btn.CssClass = "btn btn-dark form-control text-center";
    		Panel panel = new Panel();
    		panel.CssClass = "form-group";
    		panel.Controls.Add(btn);
    		panelListaCotizadores.Controls.Add(panel);
    	}

    	private void crearMSJBox(string users, string userN, string dates, string msjs, string Etapa, int tipo)
    	{
    		string User = string.Empty;
    		string UserId = string.Empty;
    		string userMsj = string.Empty;
    		string etapaMsj = string.Empty;
    		string dateMsj = string.Empty;
    		string contentMsj = string.Empty;
    		User = Convert.ToString(Session["id"]);
    		UserId = users;
    		userMsj = userN;
    		etapaMsj = Etapa;
    		dateMsj = dates;
    		contentMsj = msjs;
    		Panel panelMsjArea = new Panel();
    		Panel panelMsjData = new Panel();
    		panelMsjArea.CssClass = "clearfix align-bottom mb-5";
    		Label labelUser = new Label();
    		labelUser.Text = userMsj + " - ";
    		labelUser.CssClass = "message-data-time";
    		Label labelEtapa = new Label();
    		labelEtapa.Text = etapaMsj;
    		labelEtapa.CssClass = "message-data-time";
    		Label labelDate = new Label();
    		labelDate.Text = dateMsj;
    		labelDate.CssClass = "message-data-time";
    		Label labelMsjContent = new Label();
    		labelMsjContent.Text = contentMsj;
    		if (User == UserId)
    		{
    			switch (tipo)
    			{
    			case 1:
    				panelMsjData.CssClass = "message-data text-right";
    				labelMsjContent.CssClass = "message other-message float-right col-lg-7 col-md-9";
    				break;
    			case 2:
    				panelMsjData.CssClass = "message-data text-right";
    				labelMsjContent.CssClass = "message other-message text-white bg-danger float-right col-lg-7 col-md-9";
    				break;
    			}
    		}
    		else
    		{
    			switch (tipo)
    			{
    			case 1:
    				panelMsjData.CssClass = "message-data";
    				labelMsjContent.CssClass = "message my-message  col-lg-7 col-md-9";
    				break;
    			case 2:
    				panelMsjData.CssClass = "message-data";
    				labelMsjContent.CssClass = "message my-message text-white bg-danger col-lg-7 col-md-9";
    				break;
    			}
    		}
    		panelMsjData.Controls.Add(labelUser);
    		panelMsjData.Controls.Add(labelDate);
    		panelMsjArea.Controls.Add(panelMsjData);
    		panelMsjArea.Controls.Add(labelMsjContent);
    		panelChat.Controls.Add(panelMsjArea);
    	}

    	protected void btnPanelMensajes_Click(object sender, CommandEventArgs e)
    	{
    		try
    		{
    			if (Convert.ToInt32((string)e.CommandArgument) == 1)
    			{
    				chatArea.Enabled = true;
    				odbusuario = new BaseDatos();
    				DataSet ds = new DataSet();
    				ds = odbusuario.listaComentariosSolicitudes(Convert.ToInt32(Session["idDoc"]));
    				imgEncendido.ImageUrl = "~/userscontent/icon/botón-de-apagado.png";
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
    				imgEncendido.ImageUrl = "~/userscontent/icon/botón-de-encendido.png";
    				lbtnContorlMensajes.CommandArgument = "1";
    				chatArea.Enabled = false;
    				linkBMsj.Enabled = false;
    				txtMsj.Text = "";
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnPanelMensajes_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo desplegar correctamente los comentarios');", addScriptTags: true);
    		}
    	}

    	protected void linkBMsj_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if (!string.IsNullOrEmpty(txtMsj.Text))
    			{
    				if ((string)Session["type"] == "5")
    				{
    					int id = 0;
    					id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    					odbusuario = new BaseDatos();
    					string[] datos = new string[15];
    					datos = odbusuario.datosSolicitudes(id);
    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarComentariuosSolicitudes(null, txtMsj.Text, id, Convert.ToInt32(datos[7]), Convert.ToInt32(datos[7]), Convert.ToString(Session["id"]), 1, 1))
    					{
    						mails = new Mailer();
    						string SB = datos[1];
    						odbusuario = new BaseDatos();
    						DataSet ds = odbusuario.infoUsuarios(datos[8]);
    						string mail = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
    						string nombre = Convert.ToString(ds.Tables[0].Rows[0]["nombres"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["apellidos"]);
    						DataSet dsS = odbusuario.infoSolicitudes(id);
    						string etapa = Convert.ToString(dsS.Tables[0].Rows[0]["etapa"]);
    						string estatus = Convert.ToString(dsS.Tables[0].Rows[0]["estatus"]);
    						mails.Notify(mail, id, SB, etapa, estatus, nombre, Convert.ToString(Session["username"]), txtMsj.Text, 1);
    						CommandEventArgs commandEventArgs = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs);
    						if (datos[8] != datos[14])
    						{
    							mails = new Mailer();
    							DataSet ds2 = odbusuario.infoUsuarios(datos[14]);
    							string mail2 = Convert.ToString(ds2.Tables[0].Rows[0]["email"]);
    							string nombre2 = Convert.ToString(ds2.Tables[0].Rows[0]["nombres"]) + " " + Convert.ToString(ds2.Tables[0].Rows[0]["apellidos"]);
    							DataSet dsS2 = odbusuario.infoSolicitudes(id);
    							string etapa2 = Convert.ToString(dsS2.Tables[0].Rows[0]["etapa"]);
    							string estatus2 = Convert.ToString(dsS2.Tables[0].Rows[0]["estatus"]);
    							mails.Notify(mail, id, SB, etapa, estatus, nombre, Convert.ToString(Session["username"]), txtMsj.Text, 1);
    						}
    						txtMsj.Text = string.Empty;
    						uPanelChat.Update();
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar mensaje');", addScriptTags: true);
    						CommandEventArgs commandEventArgs2 = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs2);
    						uPanelChat.Update();
    					}
    				}
    				else if (((string)Session["type"] == "2") | ((string)Session["type"] == "3"))
    				{
    					int id2 = 0;
    					id2 = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    					odbusuario = new BaseDatos();
    					string[] datos2 = new string[14];
    					datos2 = odbusuario.datosSolicitudes(id2);
    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarComentariuosSolicitudes(null, txtMsj.Text, id2, Convert.ToInt32(datos2[7]), Convert.ToInt32(datos2[7]), Convert.ToString(Session["id"]), 1, 1))
    					{
    						mails = new Mailer();
    						string SB2 = datos2[1];
    						odbusuario = new BaseDatos();
    						DataSet ds3 = odbusuario.infoUsuarios(datos2[13]);
    						string mail3 = Convert.ToString(ds3.Tables[0].Rows[0]["email"]);
    						string nombre3 = Convert.ToString(ds3.Tables[0].Rows[0]["nombres"]) + " " + Convert.ToString(ds3.Tables[0].Rows[0]["apellidos"]);
    						DataSet dsS3 = odbusuario.infoSolicitudes(id2);
    						string etapa3 = Convert.ToString(dsS3.Tables[0].Rows[0]["etapa"]);
    						string estatus3 = Convert.ToString(dsS3.Tables[0].Rows[0]["estatus"]);
    						mails.Notify(mail3, id2, SB2, etapa3, estatus3, nombre3, Convert.ToString(Session["username"]), txtMsj.Text, 1);
    						CommandEventArgs commandEventArgs3 = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs3);
    						txtMsj.Text = string.Empty;
    						uPanelChat.Update();
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar mensaje');", addScriptTags: true);
    						CommandEventArgs commandEventArgs4 = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs4);
    						uPanelChat.Update();
    					}
    				}
    				else
    				{
    					int id3 = 0;
    					id3 = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    					odbusuario = new BaseDatos();
    					string[] datos3 = new string[14];
    					datos3 = odbusuario.datosSolicitudes(id3);
    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarComentariuosSolicitudes(null, txtMsj.Text, id3, Convert.ToInt32(datos3[7]), Convert.ToInt32(datos3[7]), Convert.ToString(Session["id"]), 1, 1))
    					{
    						CommandEventArgs commandEventArgs5 = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs5);
    						txtMsj.Text = string.Empty;
    						uPanelChat.Update();
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar mensaje');", addScriptTags: true);
    						CommandEventArgs commandEventArgs6 = new CommandEventArgs(null, "1");
    						btnPanelMensajes_Click(null, commandEventArgs6);
    						uPanelChat.Update();
    					}
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('El mensaje no puede estar vacío');", addScriptTags: true);
    				CommandEventArgs commandEventArgs7 = new CommandEventArgs(null, "1");
    				btnPanelMensajes_Click(null, commandEventArgs7);
    				txtMsj.Text = string.Empty;
    				uPanelChat.Update();
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.linkBMsj_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo registrar comentario');", addScriptTags: true);
    		}
    	}

    	protected void lbActualizarProveedor_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = Convert.ToInt32(Session["idDoc"]);
    			if ((Convert.ToInt32(dlistProveedor.SelectedIndex) != 1) & (Convert.ToInt32(dlistProveedor.SelectedIndex) != 0))
    			{
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarProveedorSolicitudes(id, Convert.ToInt32(dlistProveedor.SelectedValue), (float)Convert.ToDouble(txtPrecioProveedor.Text), txtResolucion.Text, txtOrdenCompra.Text))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Datos de proveedor actualizados');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "PorRecibir.lbActualizarProveedor_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void lbCrearProveedor_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			mpeCrearProveedor.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.lbCrearProveedor_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "PorRecibir.btnCrearProveedor_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnAsignar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			string id = "";
    			string text = "";
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.dListaCotizadores();
    			mpeCotizadores.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnAsignar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnConfirmarCotizador_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			string id = "";
    			string text = "";
    			if (dlistCotizadores.SelectedIndex > 0)
    			{
    				if (odbusuario.gestionarCotizadorSolicitudes(Convert.ToInt32(Session["idDoc"]), dlistCotizadores.SelectedValue, 1))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Cotizador asignado');", addScriptTags: true);
    					DataSet ds = new DataSet();
    					ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    					GridView1.DataSource = ds;
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
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
    			ErrorHandler.LogError(ex, "PorRecibir.btnConfirmarCotizador_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnConfirmar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = 0;
    			id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    			odbusuario = new BaseDatos();
    			if ((string)Session["type"] == "4")
    			{
    				int userRoleF = Convert.ToInt32(Session["rolesF"]);
    				odbusuario = new BaseDatos();
    				if (userRoleF == 9) // Analista de Compras: acepta en etapa 9, pasa a En Proceso
    				{
    					if (odbusuario.asignarEstatus(id, 9, 15, "Recibido por Compras — " + Convert.ToString(Session["username"]), Convert.ToString(Session["id"])))
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Confirmado');", addScriptTags: true);
    						DataSet ds = new DataSet();
    						ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    						GridView1.DataSource = ds;
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar');", addScriptTags: true);
    					}
    				}
    				else // Otros tipo 4: lógica original
    				{
    					string[] datos = new string[14];
    					datos = odbusuario.datosSolicitudes(Convert.ToInt32(id));
    					if (!string.IsNullOrEmpty(datos[13]))
    					{
    						odbusuario = new BaseDatos();
    						if (odbusuario.siguienteEtapa(Convert.ToInt32(Session["idDoc"]), 8, 4, "Enviado a cotizador ", Convert.ToString(Session["id"])))
    						{
    							ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Confirmado');", addScriptTags: true);
    							DataSet ds = new DataSet();
    							ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    							GridView1.DataSource = ds;
    							GridView1.DataBind();
    							MultiView1.ActiveViewIndex = 0;
    						}
    						else
    						{
    							ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar etapa');", addScriptTags: true);
    						}
    					}
    					else if (odbusuario.asignarEstatus(id, Convert.ToInt32(Session["idDocEtapa"]), 15, "Recibido por recepción " + Convert.ToString(Session["Recepcion"]) + " " + Convert.ToString(Session["username"]), Convert.ToString(Session["id"])))
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Confirmado');", addScriptTags: true);
    						DataSet ds2 = new DataSet();
    						ds2 = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    						GridView1.DataSource = ds2;
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar');", addScriptTags: true);
    					}
    				}
    			}
    			else if (odbusuario.asignarEstatus(id, Convert.ToInt32(Session["idDocEtapa"]), 15, "Recibido por recepción " + Convert.ToString(Session["Recepcion"]) + " " + Convert.ToString(Session["username"]), Convert.ToString(Session["id"])))
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Confirmado');", addScriptTags: true);
    				DataSet ds3 = new DataSet();
    				ds3 = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    				GridView1.DataSource = ds3;
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnConfirmar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar');", addScriptTags: true);
    		}
    	}

    	protected void btnRechazar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = 0;
    			id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    			odbusuario = new BaseDatos();
    			if (odbusuario.asignarEstatus(id, Convert.ToInt32(Session["idDocEtapa"]), 7, "Rechazado por recipción " + Convert.ToString(Session["Recepcion"]) + " " + Convert.ToString(Session["username"]), Convert.ToString(Session["id"])))
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Rechazado');", addScriptTags: true);
    				DataSet ds = new DataSet();
    				ds = filtros(null, "", "", "", null, 9, "", null, null, 1, null, null, null);
    				GridView1.DataSource = ds;
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "PorRecibir.btnRechazar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo asignar');", addScriptTags: true);
    		}
    	}
	}
}
