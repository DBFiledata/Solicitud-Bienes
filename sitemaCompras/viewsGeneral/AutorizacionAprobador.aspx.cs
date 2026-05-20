namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using sitemaCompras.Controlers;

    public partial class AutorizacionAprobador : Page
    {
    	private BaseDatos odbusuario;

    	protected ScriptManager ScriptManager1;

    	protected UpdatePanel UpdatePanel1;

    	protected HiddenField HiddenField1;

    	protected ModalPopupExtender mpeEditarRegistro;

    	protected HiddenField HiddenField2;

    	protected ModalPopupExtender mpeEliminarRegistro;

    	protected Panel Panel1;

    	protected Button btnEditarClose;

    	protected Label Label2;

    	protected Label txtEditNombre;

    	protected Label Label1;

    	protected DropDownList dListTipoAut;

    	protected Button btnEditar;

    	protected Panel Panel2;

    	protected Button btnEliminarClose;

    	protected Label lblElimiarReg;

    	protected Label Label7;

    	protected Label lblDelId;

    	protected Label Label3;

    	protected Label lblDeleteNombre;

    	protected Label Label9;

    	protected Label lblDelDirAdm;

    	protected Button btnEliminar;

    	protected MultiView MultiView1;

    	protected View viewTabla;

    	protected GridView GridView1;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if ((string)Session["type"] == "3")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterAprobador.master";
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    		}
    		else if ((string)Session["type"] != "3")
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    		if (!Page.IsPostBack)
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.filtroUsuarios("", "", "", null, Convert.ToInt32(Session["unidad"]), 2);
    			if (ds != null)
    			{
    				GridView1.DataSource = ds.Tables[0];
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    		}
    	}

    	protected void btnRegistrar_Click(object sender, EventArgs e)
    	{
    	}

    	protected void btnCancelar_Click(object sender, EventArgs e)
    	{
    		limpiarCampos();
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
    				odbusuario = new BaseDatos();
    				string[] datos = new string[9];
    				datos = odbusuario.datosUsuarios(Convert.ToString(eleccion));
    				txtEditNombre.Text = datos[1] + datos[2];
    				dListTipoAut.SelectedValue = datos[8];
    				mpeEditarRegistro.Show();
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "AutorizacionAprobador.GridView1_RowCommand", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnAddREg_Click(object sender, EventArgs e)
    	{
    		Session["idDoc"] = null;
    		Session["prosess"] = null;
    		Session["Proceso"] = 1;
    		MultiView1.ActiveViewIndex = 1;
    	}

    	protected void limpiarModalClick(object sender, EventArgs e)
    	{
    		limpiarCampos();
    		limpiarSessions();
    	}

    	protected void limpiarCampos()
    	{
    		txtEditNombre.Text = string.Empty;
    		MultiView1.ActiveViewIndex = 0;
    	}

    	protected void limpiarSessions()
    	{
    		Session["idDoc"] = null;
    		Session["prosess"] = null;
    		Session["Proceso"] = null;
    	}

    	protected void btnEditar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			if (dListTipoAut.SelectedIndex != -1)
    			{
    				string id = Convert.ToString(Session["idDoc"]);
    				if (odbusuario.gestionarAutorizacionUser(id, Convert.ToInt32(dListTipoAut.SelectedValue)))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Se actualizo la autorizacion del usuario');", addScriptTags: true);
    					limpiarCampos();
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.filtroUsuarios("", "", "", null, Convert.ToInt32(Session["unidad"]), 2);
    					if (ds != null)
    					{
    						GridView1.DataSource = ds.Tables[0];
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo editar la informacion &#10; <br />-verifique si la informacion esta ingresada correctamente');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "AutorizacionAprobador.btnEditar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnEliminar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			string id = Convert.ToString(Session["idDoc"]);
    		}
    		catch (Exception ex)
    		{
    			ErrorHandler.LogError(ex, "AutorizacionAprobador.btnEliminar_Click", "grid/button handler");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}
    }

}

