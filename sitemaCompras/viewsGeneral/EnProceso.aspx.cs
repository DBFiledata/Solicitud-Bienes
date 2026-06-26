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

    public partial class EnProceso : Page
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

    	protected ModalPopupExtender mpeReplicaResolucion;

    	protected HiddenField HiddenField14;

    	protected ModalPopupExtender mpeResultadoReplica;

    	protected HiddenField HiddenField15;

    	protected ModalPopupExtender mpeTransferir;

    	protected HiddenField HiddenField16;

    	protected ModalPopupExtender mpeConfirmarTransferir;

    	protected Panel panelConfirmarTransferir;

    	protected Button btnCloseConfirmarTransferir;

    	protected Panel panel10;

    	protected Label Label52;

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

    	protected Label Label53;

    	protected Label lblResultadoReplica;

    	protected Panel PanelReplica;

    	protected Button closeReplica;

    	protected Panel panel8;

    	protected Label msjReplica;

    	protected Label idReplicaresolucion;

    	protected Label lblCantidad;

    	protected Button btnConfirmarReplica;

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

    	protected Label Label43;

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

    	protected Label Label38;

    	protected Label txtSolicitantePrincipal;

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
    	protected HtmlGenericControl divAsignacion3Prov;
    	protected DropDownList dlistProveedor1;
    	protected TextBox txtPrecioProveedor1;
    	protected DropDownList dlistProveedor2;
    	protected TextBox txtPrecioProveedor2;
    	protected DropDownList dlistProveedor3;
    	protected TextBox txtPrecioProveedor3;
    	protected HtmlGenericControl divSeleccionDefinitiva;
    	protected RadioButtonList rblProveedores;
    	protected HtmlGenericControl divOriginalProveedor;
    	protected HtmlGenericControl divOriginalPrecio;

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

    	protected Image Image10;

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

    	protected LinkButton LbReplicaResolucion;

    	protected Image Image6;

    	protected Panel panelBtnVer;

    	protected Button btnVerdoc2;

    	protected Panel panelBtns;

    	protected Button btnVerDoc1;

    	protected Button btnEnviar;

    	protected Panel panelBtnsOthers;

    	protected Button btnSiguienteEtapa;

    	protected Button btnVerDoc;

    	protected Button btnEstatusEspeciales;

    	protected Button btnAsignar;

    	protected LinkButton LbReplicaSB;

    	protected Image Image5;

    	protected LinkButton LbTransferRenglones;

    	protected Image Image7;

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

    	protected Image Image4;

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
    			if ((string)Session["type"] == "2")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterSolicitantes.master";
    			}
    			else if ((string)Session["type"] == "3")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterAprobador.master";
    			}
    			else if ((string)Session["type"] == "4")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterRecepcionCompras.master";
    			}
    			else if ((string)Session["type"] == "5")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterCotizador.master";
    			}
    			else if ((string)Session["type"] == "6")
    			{
    				MasterPageFile = "~/viewsGeneral/NestedMasterOtrasRecepciones.master";
    			}
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    		}
    		else if ((Convert.ToInt32(Session["type"]) >= 2) & (Convert.ToInt32(Session["type"]) <= 6))
    		{
    			configurarVisibilidadPaneles();
    		}

    		if (Page.IsPostBack)
    		{
    			return;
    		}

    		if ((Convert.ToInt32(Session["type"]) >= 2) & (Convert.ToInt32(Session["type"]) <= 6))
    		{
    			DataSet ds = new DataSet();
    			ds = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
    			GridView1.DataSource = ds;
    			GridView1.DataBind();
    			MultiView1.ActiveViewIndex = 0;
    			odbusuario = new BaseDatos();
    			DataSet ds2 = odbusuario.dLiataProveedores();
    			dlistProveedor.Items.Clear();
    			dlistProveedor.DataSource = ds2;
    			dlistProveedor.DataBind();
    			dlistProveedor.Items.Insert(0, "💬 Seleccionar");

    			dlistProveedor1.Items.Clear();
    			dlistProveedor1.DataSource = ds2;
    			dlistProveedor1.DataBind();
    			dlistProveedor1.Items.Insert(0, "💬 Seleccionar");

    			dlistProveedor2.Items.Clear();
    			dlistProveedor2.DataSource = ds2;
    			dlistProveedor2.DataBind();
    			dlistProveedor2.Items.Insert(0, "💬 Seleccionar");

    			dlistProveedor3.Items.Clear();
    			dlistProveedor3.DataSource = ds2;
    			dlistProveedor3.DataBind();
    			dlistProveedor3.Items.Insert(0, "💬 Seleccionar");
    			odbusuario = new BaseDatos();
    			DataSet ds3 = odbusuario.dListaCotizadores();
    			dlistCotizadores.Items.Clear();
    			dlistCotizadores.DataSource = ds3;
    			dlistCotizadores.DataBind();
    			dlistCotizadores.Items.Insert(0, "\ud83d\udc68\ud83c\udffb\u200d\ud83d\udcbc Seleccionar");
    		}
    		else
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    	}

    	private void configurarVisibilidadPaneles()
    	{
    		if ((Convert.ToInt32(Session["type"]) == 2) | (Convert.ToInt32(Session["type"]) == 3))
    		{
    			panelAsignarProveedor.Enabled = false;
    			panelAsignarProveedor.Visible = false;
    			panelBtnsOthers.Enabled = false;
    			panelBtnsOthers.Visible = false;
    			panelBtnVer.Visible = false;
    			if (Convert.ToInt32(Session["type"]) == 2)
    			{
    				panelBtns.Visible = false;
    				panelBtnVer.Visible = true;
    			}
    		}
    		if (Convert.ToInt32(Session["type"]) == 4)
    		{
    			int userRoleF = Convert.ToInt32(Session["rolesF"]);
    			if (userRoleF == 9) // Analista de Compras
    			{
    				panelBtnsOthers.Enabled = true;
    				panelBtnsOthers.Visible = true;
    				panelBtnVer.Enabled = false;
    				panelBtnVer.Visible = false;
    				panelAsignarProveedor.Enabled = true;
    				panelAsignarProveedor.Visible = true;
    				panelInfoPreveedor.Visible = false;
    				panelInfoPreveedor.Enabled = false;
    				btnAsignar.Visible = false;
    				btnEstatusEspeciales.Visible = false;
    				btnSiguienteEtapa.Visible = true;
    			}
    			else
    			{
    				panelBtnsOthers.Enabled = false;
    				panelBtnsOthers.Visible = false;
    				panelBtnVer.Enabled = true;
    				panelBtnVer.Visible = true;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				btnAsignar.Visible = false;
    			}
    			btnActualizar.Visible = true;
    			btnActualizar.Enabled = true;
    			panelBtns.Visible = false;
    			panelBtns.Enabled = false;
    			panelInfoGeneral1.Enabled = false;
    			txtFecha.Enabled = false;
    			panelInfoGeneral3_1.Enabled = false;
    			panelInfoGeneral3_2.Enabled = false;
    			panelAddContenido.Visible = false;
    			panelAddContenido.Enabled = false;
    			LbReplicaResolucion1.Visible = true;
    			LbReplicaSB.Visible = true;
    			LbTransferRenglones.Visible = true;
    			int columnaProcesosIndex = GetColumnIndexByName(gridContenido, "Procesos");
    			if (columnaProcesosIndex != -1)
    			{
    				gridContenido.Columns[columnaProcesosIndex].Visible = false;
    			}
    		}
    		if (Convert.ToInt32(Session["type"]) == 5)
    		{
    			panelBtnsOthers.Enabled = true;
    			panelBtnsOthers.Visible = true;
    			panelAsignarProveedor.Enabled = false;
    			panelAsignarProveedor.Visible = false;
    			panelInfoPreveedor.Visible = true;
    			panelInfoPreveedor.Enabled = false;
    			btnAsignar.Visible = true;
    			btnEstatusEspeciales.Visible = false;
    			btnSiguienteEtapa.Visible = false;
    			panelBtnVer.Enabled = false;
    			panelBtnVer.Visible = false;
    			panelBtns.Visible = false;
    			panelBtns.Enabled = false;
    			panelAddContenido.Visible = false;
    			panelAddContenido.Enabled = false;
    			btnActualizar.Enabled = true;
    			btnActualizar.Visible = true;
    			LbReplicaResolucion.Visible = true;
    			LbReplicaSB.Visible = true;
    			LbTransferRenglones.Visible = true;
    			panelInfoGeneral1.Enabled = false;
    			txtFecha.Enabled = false;
    			panelInfoGeneral3_1.Enabled = false;
    			panelInfoGeneral3_2.Enabled = false;
    			int columnaProcesosIndex2 = GetColumnIndexByName(gridContenido, "Procesos");
    			if (columnaProcesosIndex2 != -1)
    			{
    				gridContenido.Columns[columnaProcesosIndex2].Visible = false;
    			}
    		}
    		if (Convert.ToInt32(Session["type"]) == 6)
    		{
    			int userRoleF = Convert.ToInt32(Session["rolesF"]);
    			panelBtnVer.Visible = false;
    			panelBtnVer.Enabled = false;
    			panelBtns.Visible = false;
    			panelBtns.Enabled = false;
    			if (userRoleF == 3) // Presupuesto
    			{
    				btnSiguienteEtapa.Text = "Enviar ✉️";
    				panelBtnsOthers.Enabled = true;
    				panelBtnsOthers.Visible = true;
    				btnEstatusEspeciales.Enabled = true;
    				btnEstatusEspeciales.Visible = true;
    				btnSiguienteEtapa.Enabled = true;
    				btnSiguienteEtapa.Visible = true;
    				btnActualizar.Enabled = false;
    				btnActualizar.Visible = false;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelInfoGeneral1.Enabled = false;
    				txtFecha.Enabled = false;
    				panelInfoGeneral3_1.Enabled = false;
    				panelInfoGeneral3_2.Enabled = false;
    				panelAddContenido.Visible = false;
    				panelAddContenido.Enabled = false;
    			}
    			else if (userRoleF == 5) // SAP
    			{
    				btnSiguienteEtapa.Text = "Enviar ✉️";
    				panelBtnsOthers.Enabled = true;
    				panelBtnsOthers.Visible = true;
    				btnEstatusEspeciales.Enabled = false;
    				btnEstatusEspeciales.Visible = false;
    				btnSiguienteEtapa.Enabled = true;
    				btnSiguienteEtapa.Visible = true;
    				
    				panelAsignarProveedor.Enabled = true;
    				panelAsignarProveedor.Visible = true;
    				dlistProveedor.Enabled = false;
    				txtPrecioProveedor.ReadOnly = true;
    				txtResolucion.ReadOnly = true;
    				txtOrdenCompra.ReadOnly = false;
    				lbActualizarProveedor.Enabled = true;
    				lbActualizarProveedor.Visible = true;
    				
    				btnActualizar.Enabled = true;
    				btnActualizar.Visible = true;
    				panelInfoGeneral1.Enabled = false;
    				txtFecha.Enabled = false;
    				panelInfoGeneral3_1.Enabled = false;
    				panelInfoGeneral3_2.Enabled = false;
    				panelAddContenido.Visible = false;
    				panelAddContenido.Enabled = false;
    			}
    			else if (userRoleF == 4) // Legal
    			{
    				btnSiguienteEtapa.Text = "Enviar ✉️";
    				panelBtnsOthers.Enabled = true;
    				panelBtnsOthers.Visible = true;
    				btnEstatusEspeciales.Enabled = true;
    				btnEstatusEspeciales.Visible = true;
    				btnSiguienteEtapa.Enabled = true;
    				btnSiguienteEtapa.Visible = true;
    				btnActualizar.Enabled = false;
    				btnActualizar.Visible = false;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelInfoGeneral1.Enabled = false;
    				txtFecha.Enabled = false;
    				panelInfoGeneral3_1.Enabled = false;
    				panelInfoGeneral3_2.Enabled = false;
    				panelAddContenido.Visible = false;
    				panelAddContenido.Enabled = false;
    			}
    			else if (userRoleF == 6) // Control Fiscal
    			{
    				btnSiguienteEtapa.Text = "Aprobar ✔️";
    				panelBtnsOthers.Enabled = true;
    				panelBtnsOthers.Visible = true;
    				btnEstatusEspeciales.Enabled = true;
    				btnEstatusEspeciales.Visible = true;
    				btnSiguienteEtapa.Enabled = true;
    				btnSiguienteEtapa.Visible = true;
    				btnActualizar.Enabled = false;
    				btnActualizar.Visible = false;
    				panelAsignarProveedor.Enabled = false;
    				panelAsignarProveedor.Visible = false;
    				panelInfoGeneral1.Enabled = false;
    				txtFecha.Enabled = false;
    				panelInfoGeneral3_1.Enabled = false;
    				panelInfoGeneral3_2.Enabled = false;
    				panelAddContenido.Visible = false;
    				panelAddContenido.Enabled = false;
    			}
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
    				odbusuario = new BaseDatos();
    				string[] datoSolicitante = new string[10];
    				datoSolicitante = odbusuario.datosUsuarios(datos[14]);
    				txtSolicitante.Text = datoSolicitante[1] + " " + datoSolicitante[2];
    				txtUnidad.Text = gvr.Cells[8].Text.ToString().Trim();
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
    				if (gvr.Cells[7].Text.ToString().Trim() != datos[8])
    				{
    					odbusuario = new BaseDatos();
    					string[] datosCreador = new string[8];
    					datosCreador = odbusuario.datosUsuarios(datos[8]);
    					txtSolicitantePrincipal.Text = datosCreador[1] + " " + datosCreador[2];
    				}
    				else
    				{
    					txtSolicitantePrincipal.Text = gvr.Cells[7].Text.ToString().Trim();
    				}
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
    				bool has3Prov = !string.IsNullOrEmpty(datos[15]) && datos[15] != "0";
    				if (has3Prov)
    				{
    					// Load 3 providers
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
    					// Load 1 provider
    					divProveedores3.Visible = false;
    					div2.Visible = true;

    					BaseDatos dbProv = new BaseDatos();
    					string[] datosProveedor = dbProv.datosProveedores(Convert.ToInt32(datos[10]));
    					lblProveedor.Text = datosProveedor != null ? datosProveedor[1] : "SIN ASIGNAR";
    					lblPrecioProveedor.Text = datos[11];
    					lblResolucion.Text = datos[2];
    					lblOrdenCompra.Text = datos[12];
    				}

    				// Configuration of Assignment/Selection Panel in Paso 6 / Paso 9
    				int userRoleF = Convert.ToInt32(Session["rolesF"]);
    				int etapaActual = Convert.ToInt32(Session["etapaActual"]);
    				if (userRoleF == 9 && etapaActual == 9) // Analista / Compras
    				{
    					panelAsignarProveedor.Enabled = true;
    					panelAsignarProveedor.Visible = true;
    					panelInfoPreveedor.Visible = false; // Hide read-only view because we are in active editing

    					if (has3Prov)
    					{
    						// Paso 9: Selección de definitivo
    						divAsignacion3Prov.Visible = false;
    						divSeleccionDefinitiva.Visible = true;
    						divOriginalProveedor.Visible = false;
    						divOriginalPrecio.Visible = false;

    						rblProveedores.Items.Clear();
    						BaseDatos dbProv = new BaseDatos();
    						string[] p1 = dbProv.datosProveedores(Convert.ToInt32(datos[15]));
    						string name1 = p1 != null ? p1[1] : "Proveedor 1";
    						
    						dbProv = new BaseDatos();
    						string[] p2 = dbProv.datosProveedores(Convert.ToInt32(datos[16]));
    						string name2 = p2 != null ? p2[1] : "Proveedor 2";

    						dbProv = new BaseDatos();
    						string[] p3 = dbProv.datosProveedores(Convert.ToInt32(datos[17]));
    						string name3 = p3 != null ? p3[1] : "Proveedor 3";

    						rblProveedores.Items.Add(new ListItem(name1 + " ($" + datos[18] + ")", datos[15]));
    						rblProveedores.Items.Add(new ListItem(name2 + " ($" + datos[19] + ")", datos[16]));
    						rblProveedores.Items.Add(new ListItem(name3 + " ($" + datos[20] + ")", datos[17]));

    						if (datos[10] != "0" && rblProveedores.Items.FindByValue(datos[10]) != null)
    						{
    							rblProveedores.SelectedValue = datos[10];
    						}
    					}
    					else
    					{
    						// Paso 6: Asignación de 3 proveedores
    						divAsignacion3Prov.Visible = true;
    						divSeleccionDefinitiva.Visible = false;
    						divOriginalProveedor.Visible = false;
    						divOriginalPrecio.Visible = false;

    						dlistProveedor1.SelectedValue = !string.IsNullOrEmpty(datos[15]) && datos[15] != "0" ? datos[15] : dlistProveedor1.Items[0].Value;
    						txtPrecioProveedor1.Text = datos[18];
    						dlistProveedor2.SelectedValue = !string.IsNullOrEmpty(datos[16]) && datos[16] != "0" ? datos[16] : dlistProveedor2.Items[0].Value;
    						txtPrecioProveedor2.Text = datos[19];
    						dlistProveedor3.SelectedValue = !string.IsNullOrEmpty(datos[17]) && datos[17] != "0" ? datos[17] : dlistProveedor3.Items[0].Value;
    						txtPrecioProveedor3.Text = datos[20];
    					}
    				}
    				else
    				{
    					// Si no es el Analista de Compras en la etapa Compras, usa la vista de lectura panelInfoPreveedor
    					panelAsignarProveedor.Enabled = false;
    					panelAsignarProveedor.Visible = false;
    					panelInfoPreveedor.Visible = true;
    					panelInfoPreveedor.Enabled = true;

    					dlistProveedor.SelectedValue = datos[10] != "" && datos[10] != "0" ? datos[10] : dlistProveedor.Items[0].Value;
    					txtPrecioProveedor.Text = datos[11];
    				}
    				txtResolucion.Text = datos[2];
    				txtOrdenCompra.Text = datos[12];
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
    			if (e.CommandName.Equals("reasignar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr3 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion3 = gvr3.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion3;
    				mpeCotizadores.Show();
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
    				string eleccion6 = "File_Compras_" + eleccion5;
    				File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion6 + ".pdf", renderedBytes);
    				string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
    				embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    				embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    				embed += "</object>";
    				ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion6 + ".pdf"));
    				MultiView1.ActiveViewIndex = 2;
    				btnCloseContent.CommandArgument = "1";
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.GridView1_RowCommand", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    	{
    		if (e.Row.RowType == DataControlRowType.DataRow)
    		{
    			Button btnVerDoc = (Button)e.Row.FindControl("Button2");
    			Button btnEliminar = (Button)e.Row.FindControl("Button4");
    			if (Convert.ToString(Session["type"]) == "4")
    			{
    				btnVerDoc.Visible = true;
    			}
    			else
    			{
    				btnVerDoc.Visible = false;
    			}
    			if ((Convert.ToString(Session["type"]) == "2") | (Convert.ToString(Session["type"]) == "3"))
    			{
    				btnEliminar.Visible = true;
    			}
    			else
    			{
    				btnEliminar.Visible = false;
    			}
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
    				odbusuario = new BaseDatos();
    				DataSet ds = new DataSet();
    				ds = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
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
    			ErrorHandler.LogError(ex, "EnProceso.btnEliminarSolicitud_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar la solicitud');", addScriptTags: true);
    		}
    	}

    	protected DataSet filtros(int? id, string solicitudBien, string resolucion, string descripciones, int? estatus, int? etapa, string usuaruio, string cotizador, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		DataSet ds = new DataSet();
    		odbusuario = new BaseDatos();
    		int userRoleF = Convert.ToInt32(Session["rolesF"]);
    		if (userRoleF == 1) // Unidad
    		{
    			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, 1, 1, usuaruio, cotizador, unidad, director, proveedor, desde, hasta);
    		}
    		else if (userRoleF == 9) // Compras (Analista de Compras)
    		{
    			int filterEstatus = estatus ?? 15;
    			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, filterEstatus, 9, usuaruio, Convert.ToString(Session["id"]), null, director, proveedor, desde, hasta);
    		}
    		else if (userRoleF == 8) // Cotizador (Supervisor de Cotizadores)
    		{
    			ds = odbusuario.filtroSolicitudesCotizadorEnproceso(id, solicitudBien, resolucion, descripciones, null, 8, usuaruio, null, null, director, proveedor, desde, hasta);
    		}
    		else // Recepciones intermedias
    		{
    			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, 15, userRoleF, usuaruio, cotizador, null, director, proveedor, desde, hasta);
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
    			if ((Convert.ToInt32(Session["type"]) == 5) | (Convert.ToInt32(Session["type"]) == 4) | (Convert.ToInt32(Session["type"]) == 6 && Convert.ToInt32(Session["rolesF"]) == 5))
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
    						btnActualizar.Visible = e != null;
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
    			ErrorHandler.LogError(ex, "EnProceso.btnActualizar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo actualizar registro');", addScriptTags: true);
    		}
    	}

    	protected void btnFiltrar_Click(object sender, EventArgs e)
    	{
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
    				ErrorHandler.LogError(ex, "EnProceso.gridContenido_RowCommand", "grid/button handler");
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
    				ErrorHandler.LogError(ex, "EnProceso.gridContenido_RowCommand", "grid/button handler");
    			}
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
    				id = Convert.ToInt32(Session["idDoc"]);
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
    					DataSet ds = new DataSet();
    					ds = odbusuario.listaDetallesSolicitudes(id);
    					gridContenido.DataSource = ds;
    					gridContenido.DataBind();
    					limpiarCamposContenido();
    					if (!div3.Visible)
    					{
    						div3.Visible = true;
    					}
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
    			ErrorHandler.LogError(ex, "EnProceso.btnAddRenglon_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo agregar renglon');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "EnProceso.btnEliminarRenglon_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnActualizarRenglon_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar renglon');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "EnProceso.gridArchivo_RowCommand", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnAddREg_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnRegistrarArchivo_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnEliminarArchivo_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo adjuntar el documento');", addScriptTags: true);
    		}
    	}

    	protected void btnSiguienteEtapa_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int userRoleF = Convert.ToInt32(Session["rolesF"]);
    			if (userRoleF == 6)
    			{
    				Session["estatusDocs"] = 3; // Aprobado
    				Session["prosesoDocs"] = 2; // Asignar estatus
    				mpeComfirmarProceso.Show();
    			}
    			else
    			{
    				configurarBotonesEtapas();
    				mpeEtapas.Show();
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.btnSiguienteEtapa_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.Etapa_Command", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.Estatus_Command", "grid/button handler");
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
    					ds2 = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
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
    					ds = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
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
    			ErrorHandler.LogError(ex, "EnProceso.btnConfirmarPorceso_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo realizar proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnEstatusEspeciales_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			configurarBotonesEstatusEspeciales();
    			mpeEstatusEspeciales.Show();
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.btnEstatusEspeciales_Click", "grid/button handler");
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

    	protected void limpiarCamposContenido()
    	{
    		txtCantidad.Text = string.Empty;
    		txtCosto.Text = string.Empty;
    		txtRenglon.Text = string.Empty;
    		txtDecsContent.Text = string.Empty;
    		txtUnidadContent.Text = string.Empty;
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
    			ErrorHandler.LogError(ex, "EnProceso.crearBtnEstatusEspeciales", "grid/button handler");
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
    					DataSet ds2 = new DataSet();
    					ds2 = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
    					GridView1.DataSource = ds2;
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo enviar');", addScriptTags: true);
    				}
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.btnEnviar_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnAprobar_Click", "grid/button handler");
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
    				labelMsjContent.CssClass = "message other-message bg-danger text-white float-right col-lg-7 col-md-9";
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
    				labelMsjContent.CssClass = "message my-message text-white  bg-danger col-lg-7 col-md-9";
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
    				panelChat.Controls.Clear(); // Limpiar mensajes previos para evitar duplicados o basura
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
    			ErrorHandler.LogError(ex, "EnProceso.btnPanelMensajes_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.linkBMsj_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo registrar comentario');", addScriptTags: true);
    		}
    	}

    	protected void lbActualizarProveedor_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = Convert.ToInt32(Session["idDoc"]);

    			if (divAsignacion3Prov.Visible)
    			{
    				if (dlistProveedor1.SelectedIndex > 0 && dlistProveedor2.SelectedIndex > 0 && dlistProveedor3.SelectedIndex > 0)
    				{
    					int prov1 = Convert.ToInt32(dlistProveedor1.SelectedValue);
    					int prov2 = Convert.ToInt32(dlistProveedor2.SelectedValue);
    					int prov3 = Convert.ToInt32(dlistProveedor3.SelectedValue);

    					if (prov1 == prov2 || prov1 == prov3 || prov2 == prov3)
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe seleccionar 3 proveedores distintos');", addScriptTags: true);
    						return;
    					}

    					float prec1 = 0, prec2 = 0, prec3 = 0;
    					if (!float.TryParse(txtPrecioProveedor1.Text, out prec1) ||
    					    !float.TryParse(txtPrecioProveedor2.Text, out prec2) ||
    					    !float.TryParse(txtPrecioProveedor3.Text, out prec3))
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe ingresar precios válidos para los 3 proveedores');", addScriptTags: true);
    						return;
    					}

    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarTresProveedoresSolicitud(id, prov1, prec1, prov2, prec2, prov3, prec3, txtResolucion.Text, txtOrdenCompra.Text))
    					{
    						Session["proveedorActual"] = prov1;
    						Session["precioActual"] = prec1;
    						Session["oCompraActual"] = txtOrdenCompra.Text;
    						btnActualizar_Click(null, null);
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar los 3 proveedores');", addScriptTags: true);
    					}
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe seleccionar los 3 proveedores');", addScriptTags: true);
    				}
    			}
    			else if (divSeleccionDefinitiva.Visible)
    			{
    				if (rblProveedores.SelectedIndex != -1)
    				{
    					int selectedProvId = Convert.ToInt32(rblProveedores.SelectedValue);
    					BaseDatos dbTemp = new BaseDatos();
    					string[] datos = dbTemp.datosSolicitudes(id);
    					float price = 0;
    					if (Convert.ToInt32(datos[15]) == selectedProvId)
    						price = float.Parse(datos[18]);
    					else if (Convert.ToInt32(datos[16]) == selectedProvId)
    						price = float.Parse(datos[19]);
    					else if (Convert.ToInt32(datos[17]) == selectedProvId)
    						price = float.Parse(datos[20]);

    					odbusuario = new BaseDatos();
    					if (odbusuario.gestionarProveedorSolicitudes(id, selectedProvId, price, txtResolucion.Text, txtOrdenCompra.Text))
    					{
    						Session["proveedorActual"] = selectedProvId;
    						Session["precioActual"] = price;
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
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe seleccionar el proveedor definitivo');", addScriptTags: true);
    				}
    			}
    			else
    			{
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
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.lbActualizarProveedor_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.lbCrearProveedor_Click", "grid/button handler");
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
    					dlistProveedor.Items.Insert(0, "💬 Seleccionar");

    					dlistProveedor1.Items.Clear();
    					dlistProveedor1.DataSource = ds;
    					dlistProveedor1.DataBind();
    					dlistProveedor1.Items.Insert(0, "💬 Seleccionar");

    					dlistProveedor2.Items.Clear();
    					dlistProveedor2.DataSource = ds;
    					dlistProveedor2.DataBind();
    					dlistProveedor2.Items.Insert(0, "💬 Seleccionar");

    					dlistProveedor3.Items.Clear();
    					dlistProveedor3.DataSource = ds;
    					dlistProveedor3.DataBind();
    					dlistProveedor3.Items.Insert(0, "💬 Seleccionar");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnCrearProveedor_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.btnAsignar_Click", "grid/button handler");
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
    				int idDoc = Convert.ToInt32(Session["idDoc"]);
    				string cotValue = dlistCotizadores.SelectedValue;
    				if (odbusuario.gestionarCotizadorSolicitudes(idDoc, cotValue, 1))
    				{
    					// Al confirmar la asignación, la solicitud avanza al Paso 6 (Etapa 9, Estatus 4)
    					odbusuario = new BaseDatos();
    					if (odbusuario.siguienteEtapa(idDoc, 9, 4, "Asignado a cotizador por supervisor " + Convert.ToString(Session["username"]), Convert.ToString(Session["id"])))
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Cotizador asignado y solicitud enviada al Paso 6');", addScriptTags: true);
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Cotizador asignado, pero no se pudo avanzar al Paso 6');", addScriptTags: true);
    					}
    					DataSet ds = filtros(null, "", "", "", null, 1, "", null, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
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
    			ErrorHandler.LogError(ex, "EnProceso.btnConfirmarCotizador_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void LbReplicaResolucion_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			msjReplica.Text = "Crear copia para resolución";
    			idReplicaresolucion.Text = "ID# " + Convert.ToString(Session["idDoc"]) + "?";
    			odbusuario = new BaseDatos();
    			string catidad = odbusuario.cantidadReplicas(Convert.ToInt32(Session["idDoc"]), 1);
    			lblCantidad.Text = "Cantidad de copias para resolución = " + catidad;
    			mpeReplicaResolucion.Show();
    			Session["tipoReplica"] = 1;
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.LbReplicaResolucion_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "EnProceso.LbReplicaSB_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnConfirmarReplica_Click1(object sender, EventArgs e)
    	{
    		try
    		{
    			int tipo = 0;
    			tipo = Convert.ToInt32(Session["tipoReplica"]);
    			switch (tipo)
    			{
    			case 1:
    			{
    				odbusuario = new BaseDatos();
    				string catidad2 = odbusuario.crearReplicaSolicitud(Convert.ToInt32(Session["idDoc"]), Convert.ToString(Session["id"]), tipo);
    				msjReplicaResultado.Text = "Se a creado la copia para resolución";
    				lblResultadoReplica.Text = "#" + catidad2;
    				mpeResultadoReplica.Show();
    				break;
    			}
    			case 2:
    			{
    				odbusuario = new BaseDatos();
    				string catidad = odbusuario.crearReplicaSolicitud(Convert.ToInt32(Session["idDoc"]), Convert.ToString(Session["id"]), tipo);
    				msjReplicaResultado.Text = "Se a creado la copia para SB";
    				lblResultadoReplica.Text = "#" + catidad;
    				mpeResultadoReplica.Show();
    				break;
    			}
    			default:
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo replicar');", addScriptTags: true);
    				break;
    			case 0:
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo replicar');", addScriptTags: true);
    				break;
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "EnProceso.btnConfirmarReplica_Click1", "grid/button handler");
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
    				DataSet ds = new DataSet();
    				ds = odbusuario.listaDetallesSolicitudes(Convert.ToInt32(eleccion));
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
    			ErrorHandler.LogError(ex, "EnProceso.LbTransferRenglones_Click", "grid/button handler");
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
    				dlistReplicaSB.Items.Insert(0, "↗\ufe0f Seleccionar");
    				mpeConfirmarTransferir.Show();
    			}
    			catch (Exception ex)
    			{
    				ErrorHandler.LogError(ex, "EnProceso.gridContenidoTransferir_RowCommand", "grid/button handler");
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
    				int id = 0;
    				int idTransfer = 0;
    				id = Convert.ToInt32(Convert.ToString(Session["idRenglonTrasnfer"]));
    				idTransfer = Convert.ToInt32(dlistReplicaSB.SelectedValue);
    				if (catidad > 0)
    				{
    					int eleccion = Convert.ToInt32(Session["idDoc"]);
    					odbusuario = new BaseDatos();
    					if (odbusuario.TransferirSB(id, idTransfer))
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Transferencia a " + idTransfer + " completa');", addScriptTags: true);
    						odbusuario = new BaseDatos();
    						DataSet ds = new DataSet();
    						ds = odbusuario.listaDetallesSolicitudes(eleccion);
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
    			ErrorHandler.LogError(ex, "EnProceso.btnTransferirReplica_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

		private void configurarBotonesEtapas()
		{
			btnUnidad.Visible = true;
			btnDirAdm.Visible = true;
			btnpresupuesto.Visible = true;
			btnLegal.Visible = true;
			btnSap.Visible = true;
			btnControlF.Visible = true;
			btnPresidencia.Visible = true;
			btnVPresidencia.Visible = true;
			btnSecretaria.Visible = true;
			btnCompras.Visible = true;
			btnContrataciones.Visible = true;

			int userRoleF = Convert.ToInt32(Session["rolesF"]);
			int etapaActual = Convert.ToInt32(Session["etapaActual"]);
			int idDoc = Convert.ToInt32(Session["idDoc"]);

			bool tieneReserva = false;
			BaseDatos db = new BaseDatos();
			DataSet dsComents = db.listaComentariosSolicitudes(idDoc);
			if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in dsComents.Tables[0].Rows)
				{
					if ((row["idEstatus"] != DBNull.Value && Convert.ToInt32(row["idEstatus"]) == 17) ||
					    (row["comentario"] != DBNull.Value && row["comentario"].ToString().ToUpper().Contains("SEPARA PARTIDA")))
					{
						tieneReserva = true;
						break;
					}
				}
			}

			string[] datos = db.datosSolicitudes(idDoc);
			string ordenCompra = datos[12];
			bool tieneOrdenCompra = !string.IsNullOrEmpty(ordenCompra);

			if (userRoleF == 3 && etapaActual == 3) // Presupuesto
			{
				ocultarTodosBotonesEtapas();
				if (!tieneReserva)
				{
					// Paso 8: Envia a Compras (Etapa 9)
					btnCompras.Visible = true;
				}
				else
				{
					// Paso 10: Envia a SAP (Etapa 5)
					btnSap.Visible = true;
				}
			}
			else if (userRoleF == 5 && etapaActual == 5) // SAP (Paso 11)
			{
				ocultarTodosBotonesEtapas();
				// Envia a Compras (Etapa 9)
				btnCompras.Visible = true;
			}
			else if (userRoleF == 8 && etapaActual == 8) // Cotizador (Paso 5)
			{
				ocultarTodosBotonesEtapas();
				// Envia a Compras (Etapa 9)
				btnCompras.Visible = true;
			}
			else if (userRoleF == 9 && etapaActual == 9) // Analista / Compras
			{
				if (tieneReserva)
				{
					ocultarTodosBotonesEtapas();
					if (!tieneOrdenCompra)
					{
						// Paso 9: Envia a Presupuesto (Etapa 3)
						btnpresupuesto.Visible = true;
					}
					else
					{
						// Difeenciación Paso 12 vs Paso 17
						bool haPasadoPorPresidencia = false;
						if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
						{
							foreach (DataRow row in dsComents.Tables[0].Rows)
							{
								if (row["descripciones"] != DBNull.Value && row["descripciones"].ToString() == "Presidencia")
								{
									haPasadoPorPresidencia = true;
									break;
								}
							}
						}

						if (haPasadoPorPresidencia)
						{
							// Paso 17: Envia a Legal (Etapa 4, Paso 18)
							btnLegal.Visible = true;
						}
						else
						{
							// Paso 12: Envia a Jefe de Compras (Etapa 9, Paso 13)
							btnCompras.Visible = true;
						}
					}
				}
			}
			else if (userRoleF == 4 && etapaActual == 4) // Legal
			{
				ocultarTodosBotonesEtapas();
				bool haPasadoPorPresidencia = false;
				int indexPresidencia = -1;
				int indexComprasFase3 = -1;

				if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
				{
					DataTable dt = dsComents.Tables[0];
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						string etapaDesc = dt.Rows[i]["descripciones"].ToString();
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
					// Paso 18: segunda vuelta → enviar a Control Fiscal (Etapa 6, Paso 19)
					btnControlF.Visible = true;
				}
				else
				{
					// Paso 16: primera vuelta → enviar a Compras (Etapa 9, Paso 17)
					btnCompras.Visible = true;
				}
			}
			else if (userRoleF == 6 && etapaActual == 6) // Control Fiscal (Paso 19)
			{
				ocultarTodosBotonesEtapas();
			}
		}

		private void ocultarTodosBotonesEtapas()
		{
			btnUnidad.Visible = false;
			btnDirAdm.Visible = false;
			btnpresupuesto.Visible = false;
			btnLegal.Visible = false;
			btnSap.Visible = false;
			btnControlF.Visible = false;
			btnPresidencia.Visible = false;
			btnVPresidencia.Visible = false;
			btnSecretaria.Visible = false;
			btnCompras.Visible = false;
			btnContrataciones.Visible = false;
		}

		private void configurarBotonesEstatusEspeciales()
		{
			btnEDocUnidad.Visible = true;
			btnEDocProveedor.Visible = true;
			btnPrecioR.Visible = true;
			btnCotizacionL.Visible = true;
			btnBloqueo.Visible = true;
			btnRechazado.Visible = true;
			Button5.Visible = true;
			Button6.Visible = true;

			Button5.Text = "Reserva de partida";
			btnBloqueo.Text = "Bloqueo";

			int userRoleF = Convert.ToInt32(Session["rolesF"]);
			int etapaActual = Convert.ToInt32(Session["etapaActual"]);
			int idDoc = Convert.ToInt32(Session["idDoc"]);

			if (userRoleF == 3 && etapaActual == 3) // Presupuesto
			{
				bool tieneReserva = false;
				BaseDatos db = new BaseDatos();
				DataSet dsComents = db.listaComentariosSolicitudes(idDoc);
				if (dsComents != null && dsComents.Tables.Count > 0 && dsComents.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dsComents.Tables[0].Rows)
					{
						if ((row["idEstatus"] != DBNull.Value && Convert.ToInt32(row["idEstatus"]) == 17) ||
						    (row["comentario"] != DBNull.Value && row["comentario"].ToString().ToUpper().Contains("SEPARA PARTIDA")))
						{
							tieneReserva = true;
							break;
						}
					}
				}

				ocultarTodosBotonesEstatusEspeciales();
				if (!tieneReserva)
				{
					// Paso 8: Habilitar "Separar Partida" (estatus 17)
					Button5.Visible = true;
					Button5.Text = "Separar Partida";
				}
				else
				{
					// Paso 10: Habilitar "Bloqueo de Partida" (estatus 12)
					btnBloqueo.Visible = true;
					btnBloqueo.Text = "Bloqueo de Partida";
				}
			}
		}

		private void ocultarTodosBotonesEstatusEspeciales()
		{
			btnEDocUnidad.Visible = false;
			btnEDocProveedor.Visible = false;
			btnPrecioR.Visible = false;
			btnCotizacionL.Visible = false;
			btnBloqueo.Visible = false;
			btnRechazado.Visible = false;
			Button5.Visible = false;
			Button6.Visible = false;
		}
	}
}
