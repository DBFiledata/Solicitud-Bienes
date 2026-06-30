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

    public partial class RegistroSolicitud : Page
    {
    	private BaseDatos odbusuario;

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

    	protected MultiView MultiView1;

    	protected View viewDescripciones;

    	protected HtmlGenericControl divNumEditar;

    	protected Label lblFivha;

    	protected Label txtUnidad;

    	protected Label lblEtapa;

    	protected TextBox txtTitulo;

    	protected Label Label1;

    	protected Label txtSolicitante;

    	protected HtmlGenericControl div1;

    	protected Label Label2;

    	protected TextBox txtSB;

    	protected Label Label3;

    	protected TextBox txtFecha;

    	protected Label Label4;

    	protected TextBox txtDescripcion;

    	protected Label Label47;

    	protected CheckBoxList checkTipoSol;

    	protected Button btnSalvar;

    	protected Button btnActualizar;

    	protected Panel panelBtnVer;

    	protected Button btnVerdoc2;

    	protected Panel panelBtnVer2;

    	protected Button btnVerdoc;

    	protected Button btnAprobar;

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

    	protected Button btnAddRenglon;

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
    		if (!Page.IsPostBack)
    		{
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
    			txtSolicitante.Text = Convert.ToString(Session["username"]);
    			txtSB.Enabled = false;
    			DataSet ds = new DataSet();
    			ds = odbusuario.infoUsuarios(Convert.ToString(Session["id"]));
    			txtUnidad.Text = ds.Tables[0].Rows[0][3].ToString();
    			MultiView1.ActiveViewIndex = 0;
    		}

    		if (Session["type"] != null && Session["type"].ToString() != "2")
    		{
    			div4.Visible = false;
    			if (gridContenido.Columns.Count > 7)
    			{
    				gridContenido.Columns[7].Visible = false;
    			}
    		}
    	}

     	protected void btnSalvar_Click(object sender, EventArgs e)
     	{
     		try
     		{
     			int checkeditemscount = checkTipoSol.Items.Cast<ListItem>().Count((ListItem li) => li.Selected);
     			int id = 0;
     			if ((!string.IsNullOrEmpty(txtUnidad.Text) & !string.IsNullOrEmpty(txtTitulo.Text) & !string.IsNullOrEmpty(txtSolicitante.Text) & !string.IsNullOrEmpty(txtFecha.Text) & !string.IsNullOrEmpty(txtDescripcion.Text)) && checkeditemscount > 0)
     			{
     				DataSet ds = new DataSet();
     				odbusuario = new BaseDatos();
     				int estatus = 0;
     				int etapa = 0;
     				if ((string)Session["type"] == "2")
     				{
     					estatus = 2;
     					etapa = 1;
     				}
     				else if ((string)Session["type"] == "3")
     				{
     					estatus = 2;
     					etapa = 1;
     				}
				odbusuario.BeginTransaction();
				ds = odbusuario.salvarSolicitudes(0, "", txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), estatus, etapa, Convert.ToString(Session["id"]), 0, txtSB.Text, 0f, "", 1);
				if (ds == null) throw new InvalidOperationException("salvarSolicitudes returned null");
     					id = Convert.ToInt32(ds.Tables[3].Rows[0][0].ToString());
     					Session["idDoc"] = id;
     					Panel1.Enabled = true;
     					Panel2.Enabled = true;
     					panelMensajes.Enabled = true;
     					btnActualizar.Visible = true;
     					btnSalvar.Visible = false;
     					if ((string)Session["type"] == "2")
     					{
     						BaseDatos odbRead = new BaseDatos();
     						string[] datosUser = new string[9];
     						datosUser = odbRead.datosUsuarios(Convert.ToString(Session["id"]));
     						if (datosUser[8] == "1")
     						{
     							panelBtnVer.Visible = true;
     							panelBtnVer.Enabled = true;
     							panelBtnVer2.Visible = false;
     							panelBtnVer2.Enabled = false;
     						}
     						else if (datosUser[8] == "2")
     						{
     							panelBtnVer.Visible = false;
     							panelBtnVer.Enabled = false;
     							panelBtnVer2.Visible = true;
     							panelBtnVer2.Enabled = true;
     							// Bug fix: Solicitante (tipo 2) no tiene capacidad de aprobación.
     							// panelBtnVer2 comparte layout con el Aprobador (tipo 3),
     							// pero el botón Aprobar jamás debe estar disponible para este rol.
     							btnAprobar.Visible = false;
     							btnAprobar.Enabled = false;
     						}
     					}
     					else if ((string)Session["type"] == "3")
     					{
     						panelBtnVer.Visible = false;
     						panelBtnVer.Enabled = false;
     						panelBtnVer2.Visible = true;
     						panelBtnVer2.Enabled = true;
     					}
     					foreach (ListItem lista in checkTipoSol.Items)
     					{
     						if (lista.Selected)
     						{
     							odbusuario.gestionarTipoSolucitudesSolicitud(id, Convert.ToInt32(lista.Value), 1);
     						}
     					}
				odbusuario.Commit();
			}
			else
     			{
     				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos de el area de generales');", addScriptTags: true);
     			}
     		}
		catch (Exception ex)
		{
			odbusuario.Rollback();
			ErrorHandler.LogError(ex, "btnSalvar_Click");
			ErrorHandler.ShowUserError(this, "guardar la solicitud", ex.Message);
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

    		// Obtener la plantilla activa desde la BD
    		odbusuario = new BaseDatos();
    		string templateRuta = odbusuario.obtenerPlantillaActiva();
    		string templatePath;
    		if (!string.IsNullOrEmpty(templateRuta))
    		{
    			templatePath = HttpContext.Current.Server.MapPath(templateRuta);
    		}
    		else
    		{
    			// Si no hay plantilla activa, generar la default automáticamente
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

    		// Embeber el HTML generado en un iframe
    		string embedHtml = "<iframe src=\"{0}\" width=\"1294px\" height=\"682px\" frameborder=\"0\"></iframe>";
    		ltEmbed.Text = string.Format(embedHtml, ResolveUrl("~/DocumentsSolicitudes/" + eleccion + ".html") + "?v=" + DateTime.Now.Ticks);
    		// ===== FIN: Nuevo flujo con plantilla Word =====

    		MultiView1.ActiveViewIndex = 1;
    	}

    	protected void btnAprobar_Click(object sender, EventArgs e)
    	{
    		// Defensa en profundidad: validar rol en el servidor.
    		// El Solicitante (tipo 2) no tiene capacidad de aprobación bajo ninguna circunstancia.
    		if ((string)Session["type"] == "2")
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No tiene permisos para realizar esta acción.');", addScriptTags: true);
    			return;
    		}
    		try
    		{
    			int proceso = 0;
    			proceso = Convert.ToInt32(Session["prosesoDocs"]);
    			int id = Convert.ToInt32(Session["idDoc"]);
    			odbusuario = new BaseDatos();
    			DataSet ds1 = new DataSet();
    			ds1 = odbusuario.listaDetallesSolicitudes(id);
    			if (ds1.Tables[0].Rows.Count > 0)
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds2 = new DataSet();
    				ds2 = odbusuario.listaDetallesSolicitudes(id);
    				if (ds2 != null)
    				{
    					odbusuario = new BaseDatos();
    					if (odbusuario.siguienteEtapa(Convert.ToInt32(Session["idDoc"]), 2, 4, "", Convert.ToString(Session["id"])))
    					{
    						limpiarCampos();
    						limpiarCamposContenido();
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Enviado a Dir. Adm');", addScriptTags: true);
    					}
    					else
    					{
    						ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo asignar etapa');", addScriptTags: true);
    					}
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe contar con información en sección de contenido y renglones');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe contar con información en sección de contenido y renglones');", addScriptTags: true);
    			}
    		}
     		catch (Exception ex)
     		{
     			ErrorHandler.LogError(ex, "btnAprobar_Click");
     			ErrorHandler.ShowUserError(this, "Error al aprobar. Contacte al administrador.");
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
     				BaseDatos odbRead = new BaseDatos();
     				string[] datos = new string[14];
     				datos = odbRead.datosSolicitudes(Convert.ToInt32(id));
     				int valorConvertido;
     				int? etapaActual = ((!string.IsNullOrWhiteSpace(datos[7]) && int.TryParse(Convert.ToString(datos[7]), out valorConvertido)) ? new int?(valorConvertido) : ((int?)null));
     				int valorConvertido2;
     				int? estatusActual = ((!string.IsNullOrWhiteSpace(datos[6]) && int.TryParse(Convert.ToString(datos[6]), out valorConvertido2)) ? new int?(valorConvertido2) : ((int?)null));
     				string UsuarioActual = ((!string.IsNullOrEmpty(datos[8])) ? datos[8] : "");
     				Session["etapaActual"] = etapaActual;
     				Session["estatusActual"] = estatusActual;
     				Session["UsuarioActual"] = UsuarioActual;
			odbusuario = new BaseDatos();
				odbusuario.BeginTransaction();
				odbusuario.gestionarSolicitudes(id, null, txtTitulo.Text, txtDescripcion.Text, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(Session["estatusActual"]), Convert.ToInt32(Session["etapaActual"]), Convert.ToString(Session["UsuarioActual"]), 0, txtSB.Text, null, null, 2);
     					btnActualizar.Visible = true;
     					btnSalvar.Visible = false;
     					if ((string)Session["type"] == "2")
     					{
     						string[] datosUser = new string[9];
     						datosUser = odbRead.datosUsuarios(Convert.ToString(Session["id"]));
     						if (datosUser[8] == "1")
     						{
     							panelBtnVer.Visible = true;
     							panelBtnVer.Enabled = true;
     							panelBtnVer2.Visible = false;
     							panelBtnVer2.Enabled = false;
     						}
     						else if (datosUser[8] == "2")
     						{
     							panelBtnVer.Visible = false;
     							panelBtnVer.Enabled = false;
     							panelBtnVer2.Visible = true;
     							panelBtnVer2.Enabled = true;
     							btnAprobar.Visible = false;
     							btnAprobar.Enabled = false;
     						}
     					}
     					else if ((string)Session["type"] == "3")
     					{
     						panelBtnVer.Visible = false;
     						panelBtnVer.Enabled = false;
     						panelBtnVer2.Visible = true;
     						panelBtnVer2.Enabled = true;
     						btnAprobar.Visible = true;
     						btnAprobar.Enabled = true;
     					}
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
				odbusuario.Commit();
				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Generales de registro actualizado');", addScriptTags: true);
     			}
     			else
     			{
     				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos de el area de generales');", addScriptTags: true);
     			}
     		}
     		catch (Exception ex)
     		{
     			ErrorHandler.LogError(ex, "btnActualizar_Click");
     			ErrorHandler.ShowUserError(this, "Error al actualizar. Contacte al administrador.");
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
     				ErrorHandler.LogError(ex, "gridContenido_RowCommand_editarRenglon");
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
     				ErrorHandler.LogError(ex, "gridContenido_RowCommand_eliminarRenglon");
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
    				float? costo = DecimalHelper.ParseFloatRobust(txtCosto.Text);
    				int valorConvertido4;
    				int? renglon = ((!string.IsNullOrWhiteSpace(txtRenglon.Text) && int.TryParse(txtRenglon.Text, out valorConvertido4)) ? new int?(valorConvertido4) : ((int?)null));
    				float? cantidad = DecimalHelper.ParseFloatRobust(txtCantidad.Text);
    				id = Convert.ToInt32(Session["idDoc"]);
    				string cantidad2 = DecimalHelper.FormatFloatForDb(cantidad);
    				string costo2 = DecimalHelper.FormatFloatForDb(costo);
    				string total2 = null;
    				id = Convert.ToInt32(Session["idDoc"]);
    				if (costo.HasValue && cantidad.HasValue)
    				{
    					total = (float)Math.Round(cantidad.Value * costo.Value, 2);
    					total2 = DecimalHelper.FormatFloatForDb(total);
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
     			ErrorHandler.LogError(ex, "btnAddRenglon_Click");
     			ErrorHandler.ShowUserError(this, "Error al agregar renglón. Contacte al administrador.");
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
     			ErrorHandler.LogError(ex, "btnEliminarRenglon_Click");
     			ErrorHandler.ShowUserError(this, "Error al eliminar renglón. Contacte al administrador.");
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
    				float? costo = DecimalHelper.ParseFloatRobust(txtCostoEdit.Text);
    				int valorConvertido4;
    				int? renglon = ((!string.IsNullOrWhiteSpace(txtRenglonEdit.Text) && int.TryParse(txtRenglonEdit.Text, out valorConvertido4)) ? new int?(valorConvertido4) : ((int?)null));
    				float? cantidad = DecimalHelper.ParseFloatRobust(txtCantidadEdit.Text);
    				string cantidad2 = DecimalHelper.FormatFloatForDb(cantidad);
    				string costo2 = DecimalHelper.FormatFloatForDb(costo);
    				string total2 = null;
    				if (costo.HasValue && cantidad.HasValue)
    				{
    					total = (float)Math.Round(cantidad.Value * costo.Value, 2);
    					total2 = DecimalHelper.FormatFloatForDb(total);
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
     			ErrorHandler.LogError(ex, "btnActualizarRenglon_Click");
     			ErrorHandler.ShowUserError(this, "Error al actualizar renglón. Contacte al administrador.");
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
     			MultiView1.ActiveViewIndex = 1;
     			}
     		}
     		catch (Exception ex)
     		{
     			ErrorHandler.LogError(ex, "gridArchivo_RowCommand");
     		}
     	}

     	protected void btnCloseContent_Click(object sender, EventArgs e)
    	{
    		MultiView1.ActiveViewIndex = 0;
    	}

    	protected void btnAddREg_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			mppeRegistrarArchivo.Show();
    		}
     		catch (Exception ex)
     		{
     			ErrorHandler.LogError(ex, "btnAddREg_Click");
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
    			string savePath = appPath + saveDir + txtNombreArchivo.Text + Convert.ToString(id) + ".pdf";
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
     			ErrorHandler.LogError(ex, "btnEliminarArchivo_Click");
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

    	protected void btnPanelMensajes_Click(object sender, EventArgs e)
    	{
    		chatArea.Enabled = true;
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
     			ErrorHandler.LogError(ex, "btnPanelMensajes_Click");
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
    					string[] datos = new string[14];
    					datos = odbusuario.datosSolicitudes(id);
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
     			ErrorHandler.LogError(ex, "linkBMsj_Click");
     		}
     	}
     }

}

