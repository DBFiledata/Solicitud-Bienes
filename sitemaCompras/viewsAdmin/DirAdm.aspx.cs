namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using sitemaCompras.Controlers;

    public partial class DirAdm : Page
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

    	protected TextBox txtEditNombre;

    	protected Label Label1;

    	protected TextBox txtEditApellido;

    	protected Label Label5;

    	protected DropDownList dListEditEstado;

    	protected Button btnEditar;

    	protected Panel Panel2;

    	protected Button btnEliminarClose;

    	protected Label lblElimiarReg;

    	protected Label Label3;

    	protected Label lblDeleteNombreDirAdm;

    	protected Button btnEliminar;

    	protected MultiView MultiView1;

    	protected View viewTabla;

    	protected Button btnAddREg;

    	protected GridView GridView1;

    	protected View viewRegistro;

    	protected Label Label4;

    	protected TextBox txtRegistroNombre;

    	protected Label Label6;

    	protected TextBox txtRegistroApellido;

    	protected Label Label8;

    	protected DropDownList dListRegistroEstado;

    	protected Button btnRegistrar;

    	protected Button btnCancelar;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if (!Page.IsPostBack && (string)Session["type"] == "1")
    		{
    			MasterPageFile = "~/viewsAdmin/superAdmin.master";
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    		}
    		else if (Convert.ToInt32(Session["type"]) != 1)
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    		if (!Page.IsPostBack)
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.dListaDirAdm();
    			if (ds != null)
    			{
    				GridView1.DataSource = ds.Tables[0];
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    			odbusuario = new BaseDatos();
    			DataSet ds5 = odbusuario.dListaEstatusUsuarios();
    			dListEditEstado.Items.Clear();
    			dListEditEstado.DataSource = ds5;
    			dListEditEstado.DataBind();
    			dListEditEstado.Items.Insert(0, "\ud83d\udccd Seleccionar");
    			dListRegistroEstado.Items.Clear();
    			dListRegistroEstado.DataSource = ds5;
    			dListRegistroEstado.DataBind();
    			dListRegistroEstado.Items.Insert(0, "\ud83d\udccd Seleccionar");
    		}
    	}

    	protected void btnRegistrar_Click(object sender, EventArgs e)
    	{
    		odbusuario = new BaseDatos();
    		if (odbusuario.gestionarDirectoresAdministrativos(0, txtRegistroNombre.Text, txtRegistroApellido.Text, Convert.ToInt32(dListRegistroEstado.SelectedValue), 1))
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.dListaDirAdm();
    			if (ds != null)
    			{
    				GridView1.DataSource = ds.Tables[0];
    				GridView1.DataBind();
    				MultiView1.ActiveViewIndex = 0;
    			}
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Se registro Dir. administrativo satisfactoriamente');", addScriptTags: true);
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
    				odbusuario = new BaseDatos();
    				string[] datos = new string[4];
    				datos = odbusuario.datosDirectoresAdministrativos(Convert.ToInt32(eleccion));
    				txtEditNombre.Text = datos[1];
    				txtEditApellido.Text = datos[2];
    				dListEditEstado.ClearSelection();
    				dListEditEstado.SelectedValue = datos[3];
    				mpeEditarRegistro.Show();
    			}
    			if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion2 = gvr2.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion2;
    				lblDeleteNombreDirAdm.Text = gvr2.Cells[1].Text.ToString().Trim();
    				mpeEliminarRegistro.Show();
    			}
    		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "DirAdm.GridView1_RowCommand", "editar/eliminar dir administrativo");
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
    		txtRegistroApellido.Text = string.Empty;
    		txtRegistroApellido.Text = string.Empty;
    		dListRegistroEstado.ClearSelection();
    		txtEditApellido.Text = string.Empty;
    		txtEditNombre.Text = string.Empty;
    		dListEditEstado.ClearSelection();
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
    			if (odbusuario.gestionarDirectoresAdministrativos(id, "", "", 0, 3))
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds = odbusuario.dListaDirAdm();
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Dir. Adm eliminado');", addScriptTags: true);
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar Dir. Adm');", addScriptTags: true);
    			}
    		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "DirAdm.btnEliminar_Click", "eliminar dir administrativo");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar Dir. Adm');", addScriptTags: true);
		}
    	}

    	protected void btnEditar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if (!string.IsNullOrEmpty(txtEditNombre.Text) & !string.IsNullOrEmpty(txtEditApellido.Text) & (dListEditEstado.SelectedIndex > 0))
    			{
    				int id = 0;
    				id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarDirectoresAdministrativos(id, txtEditNombre.Text, txtEditApellido.Text, Convert.ToInt32(dListEditEstado.SelectedValue), 2))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.dListaDirAdm();
    					if (ds != null)
    					{
    						GridView1.DataSource = ds.Tables[0];
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Dir. Adm editado');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo editar Dir. Adm');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Completar todos los campos');", addScriptTags: true);
    			}
    		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "DirAdm.btnEditar_Click", "editar dir administrativo");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar Dir. Adm');", addScriptTags: true);
		}
    	}
    }

}

