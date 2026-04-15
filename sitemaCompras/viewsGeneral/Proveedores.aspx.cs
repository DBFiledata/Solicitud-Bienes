namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using sitemaCompras.Controlers;

    public partial class Proveedores : Page
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

    	protected TextBox txtEditEmail;

    	protected Label Label5;

    	protected TextBox txtEditTelefono;

    	protected Button btnEditar;

    	protected Panel Panel2;

    	protected Button btnEliminarClose;

    	protected Label lblElimiarReg;

    	protected Label Label3;

    	protected Label lblDeleteNombreProveedor;

    	protected Button btnEliminar;

    	protected MultiView MultiView1;

    	protected View viewTabla;

    	protected Button btnAddREg;

    	protected GridView GridView1;

    	protected View view1;

    	protected Label Label4;

    	protected TextBox txtRegistroNombre;

    	protected Label Label6;

    	protected TextBox txtRegistroEmail;

    	protected Label Label7;

    	protected TextBox txtTelefono;

    	protected Button Button3;

    	protected Button Button4;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Response.Redirect("~/Login.aspx");
    		}
    		else if ((string)Session["type"] != "5")
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    		if (!Page.IsPostBack)
    		{
    			odbusuario = new BaseDatos();
    			DataSet ds = odbusuario.listaProveedores();
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
    		try
    		{
    			if (!string.IsNullOrEmpty(txtRegistroNombre.Text) & !string.IsNullOrEmpty(txtRegistroEmail.Text) & !string.IsNullOrEmpty(txtTelefono.Text))
    			{
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarProveedores(null, txtRegistroNombre.Text, txtRegistroEmail.Text, txtTelefono.Text, 1))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.listaProveedores();
    					if (ds != null)
    					{
    						GridView1.DataSource = ds.Tables[0];
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Proveedor Registrado');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo registrar proveedor');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar proveedor');", addScriptTags: true);
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
    				datos = odbusuario.datosProveedores(Convert.ToInt32(eleccion));
    				txtEditNombre.Text = datos[1];
    				txtEditEmail.Text = datos[2];
    				txtEditTelefono.Text = datos[3];
    				mpeEditarRegistro.Show();
    			}
    			if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion2 = gvr2.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion2;
    				odbusuario = new BaseDatos();
    				string[] datos2 = new string[4];
    				datos2 = odbusuario.datosProveedores(Convert.ToInt32(eleccion2));
    				lblDeleteNombreProveedor.Text = datos2[1];
    				mpeEliminarRegistro.Show();
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnEditar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			if (!string.IsNullOrEmpty(txtEditNombre.Text) & !string.IsNullOrEmpty(txtEditEmail.Text) & !string.IsNullOrEmpty(txtEditTelefono.Text))
    			{
    				int id = 0;
    				id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    				odbusuario = new BaseDatos();
    				if (odbusuario.gestionarProveedores(id, txtEditNombre.Text, txtEditEmail.Text, txtEditTelefono.Text, 2))
    				{
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.listaProveedores();
    					if (ds != null)
    					{
    						GridView1.DataSource = ds.Tables[0];
    						GridView1.DataBind();
    						MultiView1.ActiveViewIndex = 0;
    					}
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Proveedor editado');", addScriptTags: true);
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo editar proveedor');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Completar todos los campos');", addScriptTags: true);
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo editar proveedor');", addScriptTags: true);
    		}
    	}

    	protected void btnEliminar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			int id = 0;
    			id = Convert.ToInt32(Convert.ToString(Session["idDoc"]));
    			odbusuario = new BaseDatos();
    			if (odbusuario.gestionarProveedores(id, "", "", "", 3))
    			{
    				odbusuario = new BaseDatos();
    				DataSet ds = odbusuario.listaProveedores();
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Proveedor eliminado');", addScriptTags: true);
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar proveedor');", addScriptTags: true);
    			}
    		}
    		catch (Exception)
    		{
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error no se pudo eliminar proveedor');", addScriptTags: true);
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
    		MultiView1.ActiveViewIndex = 0;
    	}

    	protected void limpiarSessions()
    	{
    		Session["idDoc"] = null;
    		Session["prosess"] = null;
    		Session["Proceso"] = null;
    	}
    }

}

