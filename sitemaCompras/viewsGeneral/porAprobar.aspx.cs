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

    public partial class porAprobar : Page
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

    	protected HiddenField HiddenField9;

    	protected ModalPopupExtender mpeComfirmarProceso;

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

    	protected Panel panelConfirmarProceso;

    	protected Button closeConfirmarProceso;

    	protected Panel panel7;

    	protected Label Label33;

    	protected TextBox txtComentario;

    	protected Button btnConfirmarPorceso;

    	protected MultiView MultiView1;

    	protected View ViewTabla;

    	protected Panel Panel3;

    	protected Label Label24;

    	protected TextBox txtId;

    	protected Label Label45;

    	protected TextBox txtTituloF;

    	protected Label Label51;

    	protected DropDownList dlistUsuario;

    	protected Button btnFiltrar;

    	protected GridView GridView1;

    	protected View viewDescripciones;

    	protected HtmlGenericControl divNumEditar;

    	protected Label lblFicha;

    	protected Label txtUnidad;

    	protected Label lblEtapa;

    	protected TextBox txtTitulo;

    	protected Label Label1;

    	protected Label txtSolicitante;

    	protected HtmlGenericControl div1;

    	protected Label Label7;

    	protected Label txtSolicitantePrincipal;

    	protected Label Label2;

    	protected TextBox txtSB;

    	protected Label Label3;

    	protected TextBox txtFecha;

    	protected Label Label4;

    	protected TextBox txtDescripcion;

    	protected Label Label47;

    	protected CheckBoxList checkTipoSol;

    	protected Button btnActualizar;

    	protected Panel panelBtns;

    	protected Button btnVerDoc;

    	protected Button btnAprobar;

    	protected Panel panelVer;

    	protected Button btnVerDoc1;

    	protected Panel Panel1;

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
    		if ((string)Session["type"] == "2")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterSolicitantes.master";
    		}
    		else if ((string)Session["type"] == "3")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterAprobador.master";
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
    		else if (((string)Session["type"] != "2") & ((string)Session["type"] != "3"))
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    		odbusuario = new BaseDatos();
    		if (Convert.ToInt32(Session["type"]) == 2)
    		{
    			odbusuario = new BaseDatos();
    			string[] datosUser = new string[9];
    			datosUser = odbusuario.datosUsuarios(Convert.ToString(Session["id"]));
    			if (datosUser[8] == "1")
    			{
    				panelVer.Visible = true;
    				panelVer.Enabled = true;
    				panelBtns.Visible = false;
    				panelBtns.Enabled = false;
    			}
    			else if (datosUser[8] == "2")
    			{
    				panelVer.Visible = false;
    				panelVer.Enabled = false;
    				panelBtns.Visible = true;
    				panelBtns.Enabled = true;
    			}
    		}
    		if (Convert.ToInt32(Session["type"]) == 3)
    		{
    			panelVer.Visible = false;
    		}
    		txtSB.Enabled = false;
    		DataSet ds = new DataSet();
    		int userRoleF = Convert.ToInt32(Session["rolesF"]);
    		if (userRoleF == 1) // UNIDAD: etapa 1, estatus 2 (En espera de Aprobación), filtro por unidad
    		{
    			ds = filtros(null, "", "", "", 2, 1, "", Convert.ToInt32(Session["unidad"]), 1, null, null, null);
    		}
    		else // DIR. ADM y otros: etapa = rolesF, estatus 4 (Enviado), sin filtro de unidad
    		{
    			ds = filtros(null, "", "", "", 4, userRoleF, "", null, 1, null, null, null);
    		}
    		GridView1.DataSource = ds;
    		GridView1.DataBind();
    		dlistUsuario.Items.Clear();
    		DataSet dsUsers = odbusuario.filtroUsuarios("", "", "", null, Convert.ToInt32(Session["unidad"]), null);
    		dlistUsuario.Enabled = true;
    		dlistUsuario.DataSource = dsUsers;
    		dlistUsuario.DataBind();
    		dlistUsuario.Items.Insert(0, "\ud83d\udc68\ud83c\udffb\u200d\ud83d\udcbc Seleccionar");
    		MultiView1.ActiveViewIndex = 0;
    	}

    	protected DataSet filtros(int? id, string solicitudBien, string resolucion, string descripciones, int? estatus, int? etapa, string usuaruio, int? unidad, int? director, int? proveedor, DateTime? desde, DateTime? hasta)
    	{
    		DataSet ds = new DataSet();
    		odbusuario = new BaseDatos();
    		int userRoleF = Convert.ToInt32(Session["rolesF"]);
    		if (userRoleF == 1 && Convert.ToInt32(Session["type"]) == 2)
    		{
    			// Solicitante de UNIDAD: solo ve sus propias solicitudes
    			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, estatus, etapa, Convert.ToString(Session["id"]), null, unidad, director, proveedor, desde, hasta);
    		}
    		else
    		{
    			// Aprobadores (UNIDAD, DIR. ADM, etc.): ven solicitudes de su etapa
    			ds = odbusuario.filtroSolicitudes(id, solicitudBien, resolucion, descripciones, estatus, etapa, usuaruio, null, unidad, director, proveedor, desde, hasta);
    		}
    		return ds;
    	}

    	protected void btnFiltrar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int? id = null;
    			string titulo = "";
    			string usuaruio = "";
    			id = VerificarCampoIntNull(txtId);
    			usuaruio = VerificarDListText(dlistUsuario);
    			titulo = VerificarCampo(txtTituloF);
    			int filterRoleF = Convert.ToInt32(Session["rolesF"]);
    			DataSet ds;
    			if (filterRoleF == 1)
    			{
    				ds = filtros(id, "", "", titulo, 2, 1, usuaruio, Convert.ToInt32(Session["unidad"]), 1, null, null, null);
    			}
    			else
    			{
    				ds = filtros(id, "", "", titulo, 4, filterRoleF, usuaruio, null, 1, null, null, null);
    			}
    			GridView1.DataSource = ds;
    			GridView1.DataBind();
    			MultiView1.ActiveViewIndex = 0;
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "porAprobar.btnFiltrar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo aplicar filtro');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "porAprobar.VerificarDListText", "grid/button handler");
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
     					odbusuario.BeginTransaction();
     						odbusuario.gestionarArchivosSolicitudes(null, txtNombreArchivo.Text, txtNombreArchivo.Text + Convert.ToString(id) + ".pdf", id, Convert.ToString(Session["id"]), 1);
     						FileUpload1.SaveAs(savePath);
     						odbusuario.Commit();
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
     			odbusuario.Rollback();
     			ErrorHandler.LogError(ex, "btnRegistrarArchivo_Click");
     			ErrorHandler.ShowUserError(this, "Error al registrar archivo. Contacte al administrador.");
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
    			ErrorHandler.LogError(ex, "porAprobar.btnEliminarArchivo_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo adjuntar el documento');", addScriptTags: true);
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
    				int filterRoleF = Convert.ToInt32(Session["rolesF"]);
    				if (filterRoleF == 1)
    				{
    					ds = filtros(null, "", "", "", 2, 1, "", Convert.ToInt32(Session["unidad"]), 1, null, null, null);
    				}
    				else
    				{
    					ds = filtros(null, "", "", "", 4, filterRoleF, "", null, 1, null, null, null);
    				}
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
    			ErrorHandler.LogError(ex, "porAprobar.btnEliminarSolicitud_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar la solicitud');", addScriptTags: true);
    		}
    	}

     	protected void btnConfirmarPorceso_Click(object sender, EventArgs e)
     	{
     		try
     		{
     			int proceso = 0;
     			int etapa = 0;
     			int estatus = 0;
     			proceso = Convert.ToInt32(Session["prosesoDocs"]);
     			int id = Convert.ToInt32(Session["idDoc"]);
     			odbusuario = new BaseDatos();
     			DataSet ds1 = new DataSet();
     			ds1 = odbusuario.listaDetallesSolicitudes(id);
     			if (ds1.Tables[0].Rows.Count > 0)
     			{
     				odbusuario = new BaseDatos();
     				odbusuario.BeginTransaction();
     					switch (proceso)
     					{
     					case 1:
     						etapa = Convert.ToInt32(Session["etapaDocs"]);
     						if (odbusuario.siguienteEtapa(Convert.ToInt32(Session["idDoc"]), etapa, 4, txtComentario.Text, Convert.ToString(Session["id"])))
     						{
     							odbusuario.Commit();
     							odbusuario = new BaseDatos();
     							DataSet ds3 = new DataSet();
     							int filterRoleF = Convert.ToInt32(Session["rolesF"]);
     							if (filterRoleF == 1)
     							{
     								ds3 = filtros(null, "", "", "", 2, 1, "", Convert.ToInt32(Session["unidad"]), 1, null, null, null);
     							}
     							else
     							{
     								ds3 = filtros(null, "", "", "", 4, filterRoleF, "", null, 1, null, null, null);
     							}
     							GridView1.DataSource = ds3;
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
     						if (odbusuario.asignarEstatus(Convert.ToInt32(Session["idDoc"]), etapaA, estatus, txtComentario.Text, Convert.ToString(Session["id"])))
     						{
     							odbusuario.Commit();
     							odbusuario = new BaseDatos();
     							DataSet ds2 = new DataSet();
     							int filterRoleF2 = Convert.ToInt32(Session["rolesF"]);
     							if (filterRoleF2 == 1)
     							{
     								ds2 = filtros(null, "", "", "", 2, 1, "", Convert.ToInt32(Session["unidad"]), 1, null, null, null);
     							}
     							else
     							{
     								ds2 = filtros(null, "", "", "", 4, filterRoleF2, "", null, 1, null, null, null);
     							}
     							GridView1.DataSource = ds2;
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
      			else
     			{
     				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe contar con información en sección de contenido y renglones');", addScriptTags: true);
     			}
     		}
     		catch (Exception ex)
     		{
     			odbusuario.Rollback();
     			ErrorHandler.LogError(ex, "btnConfirmarPorceso_Click");
     			ErrorHandler.ShowUserError(this, "Error al confirmar proceso. Contacte al administrador.");
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
    				odbusuario = new BaseDatos();
    				string[] datos = new string[14];
    				datos = odbusuario.datosSolicitudes(Convert.ToInt32(eleccion));
    				txtUnidad.Text = gvr.Cells[8].Text.ToString().Trim();
    				txtSolicitante.Text = gvr.Cells[7].Text.ToString().Trim();
    				txtTitulo.Text = datos[3];
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
    			if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr3 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion3 = gvr3.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion3;
    				lblEliminarSolicitud.Text = gvr3.Cells[0].Text.ToString().Trim();
    				lblTituloEliminar.Text = gvr3.Cells[2].Text.ToString().Trim();
    				mpeEliminarSolicitud.Show();
    			}
    			if (e.CommandName.Equals("verDoc", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr4 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion4 = gvr4.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion4;
    				DataSet ds4 = new DataSet();
    				odbusuario = new BaseDatos();
    				ds4 = odbusuario.reporteBody(Convert.ToInt32(eleccion4));

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
    				string eleccion5 = "File_Compras_" + eleccion4;
    				File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion5 + ".pdf", renderedBytes);
    				string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1294px\" height=\"682px\">";
    				embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    				embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    				embed += "</object>";
    				ltEmbed.Text = string.Format(embed, ResolveUrl("~/DocumentsSolicitudes/" + eleccion5 + ".pdf"));
    				*/
    				// ===== FIN: Código Telerik Spreadsheet original =====

    				// ===== INICIO: Nuevo flujo con plantilla Word (.docx) =====
    				string eleccion5 = "File_Compras_" + eleccion4;
    				string htmlPath = HttpContext.Current.Server.MapPath("~/DocumentsSolicitudes/") + eleccion5 + ".html";
    				odbusuario = new BaseDatos();
    				string templateRuta = odbusuario.obtenerPlantillaActiva();
    				string templatePath;
    				if (!string.IsNullOrEmpty(templateRuta))
    				{
    					templatePath = HttpContext.Current.Server.MapPath(templateRuta);
    				}
    				else
    				{
    					templatePath = HttpContext.Current.Server.MapPath("~/DocumentsPlantillas/plantilla_default.docx");
    					if (!File.Exists(templatePath))
    					{
    						ReporteComprasDocx generador = new ReporteComprasDocx();
    						generador.CrearPlantillaDefault(
    							templatePath,
    							HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png"),
    							HttpContext.Current.Server.MapPath("~/userscontent/img/header.png"),
    							HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png")
    						);
    					}
    				}
    				ReporteComprasDocx reporteDocx = new ReporteComprasDocx();
    				reporteDocx.GenerarDocumentoDesdePlantilla(ds4, templatePath, htmlPath);
    				string embedHtml = "<iframe src=\"{0}\" width=\"1294px\" height=\"682px\" frameborder=\"0\"></iframe>";
    				ltEmbed.Text = string.Format(embedHtml, ResolveUrl("~/DocumentsSolicitudes/" + eleccion5 + ".html") + "?v=" + DateTime.Now.Ticks);
    				// ===== FIN: Nuevo flujo con plantilla Word =====

    				MultiView1.ActiveViewIndex = 2;
    				btnCloseContent.CommandArgument = "1";
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "porAprobar.GridView1_RowCommand", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnActualizar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int checkeditemscount = checkTipoSol.Items.Cast<ListItem>().Count((ListItem li) => li.Selected);
    			int id = 0;
    			if ((!string.IsNullOrEmpty(txtUnidad.Text) & !string.IsNullOrEmpty(txtTitulo.Text) & !string.IsNullOrEmpty(txtSolicitante.Text) & !string.IsNullOrEmpty(txtFecha.Text) & !string.IsNullOrEmpty(txtDescripcion.Text)) && checkeditemscount > 0)
    			{
    				id = Convert.ToInt32(Session["idDoc"]);
    				float valorConvertido1;
    				float? precioActual = ((!string.IsNullOrWhiteSpace(Convert.ToString(Session["precioActual"])) && float.TryParse(Convert.ToString(Session["precioActual"]), out valorConvertido1)) ? new int?((int)valorConvertido1) : ((int?)null));
    				int valorConvertido2;
    				int? proveedorActual = ((!string.IsNullOrWhiteSpace(Convert.ToString(Session["proveedorActual"])) && int.TryParse(Convert.ToString(Session["proveedorActual"]), out valorConvertido2)) ? new int?(valorConvertido2) : ((int?)null));
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarSolicitudes(id, Convert.ToString(Session["resolucionActual"]), txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(Session["estatusActual"]), Convert.ToInt32(Session["etapaActual"]), Convert.ToString(Session["UsuarioActual"]), proveedorActual, txtSB.Text, precioActual, Convert.ToString(Session["oCompraActual"]), 2))
    				{
    					btnActualizar.Visible = true;
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
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "porAprobar.btnActualizar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo actualizar registro');", addScriptTags: true);
    		}
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
    		{
    			templatePath = HttpContext.Current.Server.MapPath(templateRuta);
    		}
    		else
    		{
    			templatePath = HttpContext.Current.Server.MapPath("~/DocumentsPlantillas/plantilla_default.docx");
    			if (!File.Exists(templatePath))
    			{
    				ReporteComprasDocx generador = new ReporteComprasDocx();
    				generador.CrearPlantillaDefault(
    					templatePath,
    					HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png"),
    					HttpContext.Current.Server.MapPath("~/userscontent/img/header.png"),
    					HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png")
    				);
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

    	protected void btnAprobar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int userRoleF = Convert.ToInt32(Session["rolesF"]);
    			int siguienteEtapa = 2;
    			if (userRoleF == 2)
    			{
    				siguienteEtapa = 9;
    			}
    			else if (userRoleF == 9)
    			{
    				siguienteEtapa = 8; // Jefe de Compras (9) aprueba y envía al Supervisor de Cotizadores (8)
    			}

    			if ((string)Session["type"] == "3")
    			{
    				Session["etapaDocs"] = siguienteEtapa;
    				Session["prosesoDocs"] = 1;
    				mpeComfirmarProceso.Show();
    			}
    			else if ((string)Session["type"] == "2")
    			{
    				odbusuario = new BaseDatos();
    				string[] datosUser = new string[9];
    				datosUser = odbusuario.datosUsuarios(Convert.ToString(Session["id"]));
    				if (datosUser[8] == "2")
    				{
    					Session["etapaDocs"] = siguienteEtapa;
    					Session["prosesoDocs"] = 1;
    					mpeComfirmarProceso.Show();
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no esta autorizado para realizar este proseso');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no esta autorizado para realizar este proseso');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "porAprobar.btnAprobar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se generar lista');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "porAprobar.btnAddRenglon_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "porAprobar.btnEliminarRenglon_Click", "grid/button handler");
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
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo editar la el renglon');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "porAprobar.btnActualizarRenglon_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar renglon');", addScriptTags: true);
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
     				ErrorHandler.LogError(ex, "porAprobar.gridContenido_RowCommand", "grid/button handler");
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
     				ErrorHandler.LogError(ex, "porAprobar.gridContenido_RowCommand", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "porAprobar.btnAddREg_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar a proceso');", addScriptTags: true);
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
    			ErrorHandler.LogError(ex, "porAprobar.gridArchivo_RowCommand", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "porAprobar.btnPanelMensajes_Click", "grid/button handler");
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
    			ErrorHandler.LogError(ex, "porAprobar.linkBMsj_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo registrar comentario');", addScriptTags: true);
    		}
    	}

    	protected void btnCloseContent_Click(object sender, EventArgs e)
    	{
    		MultiView1.ActiveViewIndex = 1;
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

    	protected void limpiarCamposContenido()
    	{
    		txtCantidad.Text = string.Empty;
    		txtCosto.Text = string.Empty;
    		txtRenglon.Text = string.Empty;
    		txtDecsContent.Text = string.Empty;
    		txtUnidadContent.Text = string.Empty;
    	}
	}
}