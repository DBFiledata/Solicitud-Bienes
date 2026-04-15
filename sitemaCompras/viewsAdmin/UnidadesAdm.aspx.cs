namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using sitemaCompras.Controlers;

    public partial class UnidadesAdm : Page
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

    	protected TextBox txtRevision;

    	protected Button btnEditar;

    	protected Panel Panel2;

    	protected Button btnEliminarClose;

    	protected Label Label3;

    	protected Label lblNombreUnidadAdm;

    	protected Button btnEliminar;

    	protected MultiView MultiView1;

    	protected View viewTabla;

    	protected Button btnAddREg;

    	protected GridView GridView1;

    	protected View viewRegistro;

    	protected Label Label7;

    	protected TextBox txtId;

    	protected Button btnRegistrar;

    	protected Button btnCancelar;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if (!Page.IsPostBack)
    		{
    			if ((string)Session["type"] == "1")
    			{
    				MasterPageFile = "~/viewsAdmin/superAdmin.master";
    			}
    			else if ((string)Session["type"] == "7")
    			{
    				MasterPageFile = "~/viewsAdmin/adminUsers.master";
    			}
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    		}
    		if ((Convert.ToInt32(Session["type"]) == 1) | (Convert.ToInt32(Session["type"]) == 7))
    		{
    			if (!Page.IsPostBack)
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds = odbusuario.dListaUnidadesSolicitantes();
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
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

    	protected void btnRegistrar_Click(object sender, EventArgs e)
    	{
    		odbusuario = new BaseDatos();
    		if (odbusuario.gestionarUnidadesSolicitantes(0, txtId.Text, 1))
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.dListaUnidadesSolicitantes();
    			if (ds != null)
    			{
    				GridView1.DataSource = ds.Tables[0];
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Se registro unidad administrativa satisfactoriamente');", addScriptTags: true);
    			limpiarCampos();
    		}
    		else
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo registrar la informacion &#10; <br />-verifique si la informacion esta ingresada correctamente');", addScriptTags: true);
    		}
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
    				txtRevision.Text = gvr.Cells[1].Text.ToString().Trim();
    				mpeEditarRegistro.Show();
    			}
    			if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion2 = gvr2.Cells[1].Text.ToString().Trim();
    				Session["idDoc"] = eleccion2;
    				lblNombreUnidadAdm.Text = gvr2.Cells[1].Text.ToString().Trim();
    				mpeEliminarRegistro.Show();
    			}
    		}
    		catch (Exception)
    		{
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
    		txtId.Text = string.Empty;
    		MultiView1.ActiveViewIndex = 0;
    	}

    	protected void limpiarSessions()
    	{
    		Session["idDoc"] = null;
    		Session["prosess"] = null;
    		Session["Proceso"] = null;
    	}

    	protected void btnEliminar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = 0;
    			id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    			odbusuario = new BaseDatos();
    			if (odbusuario.gestionarUnidadesSolicitantes(id, "", 3))
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds = odbusuario.dListaUnidadesSolicitantes();
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Unidad administrativa eliminada');", addScriptTags: true);
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar unidad administrativa');", addScriptTags: true);
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar unidad administrativa');", addScriptTags: true);
    		}
    	}

    	protected void btnEditar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if (!string.IsNullOrEmpty(txtRevision.Text))
    			{
    				int id = 0;
    				id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarUnidadesSolicitantes(id, txtRevision.Text, 2))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.dListaUnidadesSolicitantes();
    					if (ds != null)
    					{
    						GridView1.DataSource = ds.Tables[0];
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Unidad administrativa editada');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo editar unidad administrativa');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar unidad administrativa');", addScriptTags: true);
    		}
    	}
    }

}

