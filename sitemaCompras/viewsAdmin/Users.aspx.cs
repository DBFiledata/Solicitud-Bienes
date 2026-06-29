namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using sitemaCompras.Controlers;

    public partial class Users : Page
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

    	protected Label Label15;

    	protected TextBox txtEditId;

    	protected Label Label4;

    	protected TextBox txtEditPassword;

    	protected Label Label2;

    	protected TextBox txtEditNombre;

    	protected Label Label1;

    	protected TextBox txtEditApellido;

    	protected Label Label18;

    	protected TextBox txtEditTelefono;

    	protected Label Label19;

    	protected TextBox txtEditEmail;

    	protected Label Label5;

    	protected DropDownList dlistEditUnidad;

    	protected Label Label16;

    	protected DropDownList dListEditTipo;

    	protected Label Label17;

    	protected DropDownList dListEditEstado;

    	protected DropDownList dListEditRolesF;

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

    	protected Button btnAddREg;

    	protected GridView GridView1;

    	protected View viewRegistro;

    	protected Label Label;

    	protected TextBox txtId;

    	protected Label Label20;

    	protected TextBox txtpass;

    	protected Label Label6;

    	protected TextBox txtRegistroNombre;

    	protected Label Label8;

    	protected TextBox txtRegistroApellido;

    	protected Label Label10;

    	protected TextBox txtRegistrarTelefono;

    	protected Label Label11;

    	protected TextBox txtRegistrarEmail;

    	protected Label Label12;

    	protected DropDownList dListUnidad;

    	protected Label Label13;

    	protected DropDownList dListTipo;

    	protected Label Label14;

    	protected DropDownList dListEstado;

    	protected DropDownList dListRolesF;

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
    				DataSet ds = odbusuario.filtroUsuarios("", "", "", null, null, null);
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    				odbusuario = new BaseDatos();
    				DataSet ds2 = odbusuario.dListaUnidadesSolicitantes();
    				dListUnidad.Items.Clear();
    				dListUnidad.DataSource = ds2;
    				dListUnidad.DataBind();
    				dListUnidad.Items.Insert(0, "\ud83d\udccd Seleccionar");
    				dlistEditUnidad.Items.Clear();
    				dlistEditUnidad.DataSource = ds2;
    				dlistEditUnidad.DataBind();
    				dlistEditUnidad.Items.Insert(0, "\ud83d\udccd Seleccionar");
    				odbusuario = new BaseDatos();
    				DataSet ds3 = odbusuario.dListaTipoUsuario();
    				dListTipo.Items.Clear();
    				dListTipo.DataSource = ds3;
    				dListTipo.DataBind();
    				dListTipo.Items.Insert(0, "\ud83e\udd35\u200d♂\ufe0f Seleccionar");
    				dListEditTipo.Items.Clear();
    				dListEditTipo.DataSource = ds3;
    				dListEditTipo.DataBind();
    				dListEditTipo.Items.Insert(0, "\ud83e\udd35\u200d♂\ufe0f Seleccionar");
    				odbusuario = new BaseDatos();
    				DataSet ds4 = odbusuario.dListaEstatusUsuarios();
    				dListEstado.Items.Clear();
    				dListEstado.DataSource = ds4;
    				dListEstado.DataBind();
    				dListEstado.Items.Insert(0, "\ud83d\udea9 Seleccionar");
    				dListEditEstado.Items.Clear();
    				dListEditEstado.DataSource = ds4;
    				dListEditEstado.DataBind();
    				dListEditEstado.Items.Insert(0, "\ud83d\udea9 Seleccionar");
    				odbusuario = new BaseDatos();
    				DataSet ds5 = odbusuario.dListaRolesF();
    				dListRolesF.Items.Clear();
    				dListRolesF.DataSource = ds5;
    				dListRolesF.DataBind();
    				dListRolesF.Items.Insert(0, "🔄 Seleccionar");
    				dListEditRolesF.Items.Clear();
    				dListEditRolesF.DataSource = ds5;
    				dListEditRolesF.DataBind();
    				dListEditRolesF.Items.Insert(0, "🔄 Seleccionar");
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
    		try
    		{
    			odbusuario = new BaseDatos();
    			if (!string.IsNullOrEmpty(txtId.Text) & !string.IsNullOrEmpty(txtRegistroNombre.Text) & !string.IsNullOrEmpty(txtRegistroApellido.Text) & !string.IsNullOrEmpty(txtpass.Text) & (dListEstado.SelectedIndex > 0) & (dListUnidad.SelectedIndex > 0) & (dListTipo.SelectedIndex > 0))
    			{
    				int? rolesF = null;
    				if (dListRolesF.SelectedIndex > 0)
    				{
    					rolesF = Convert.ToInt32(dListRolesF.SelectedValue);
    				}
    				if (odbusuario.gestionarUsuarios(txtId.Text, "", txtRegistroNombre.Text, txtRegistroApellido.Text, txtpass.Text, txtRegistrarTelefono.Text, txtRegistrarEmail.Text, Convert.ToInt32(dListEstado.SelectedValue), Convert.ToInt32(dListUnidad.SelectedValue), Convert.ToInt32(dListTipo.SelectedValue), rolesF, 1))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Registro de Usuario Completado');", addScriptTags: true);
    					limpiarCampos();
    				}
    				else
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo registrar la informacion &#10; <br />-verifique si la informacion esta ingresada correctamente');", addScriptTags: true);
    				}
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos &#10;');", addScriptTags: true);
    			}
    		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "Users.btnRegistrar_Click", "registrar usuario");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error BD: " + ex.Message.Replace("'", ".").Replace("\r", "").Replace("\n", " ") + "');", addScriptTags: true);
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
    				string[] datos = new string[10];
    				datos = odbusuario.datosUsuarios(Convert.ToString(eleccion));
    				txtEditId.Text = Convert.ToString(eleccion);
    				txtEditNombre.Text = datos[1];
    				txtEditApellido.Text = datos[2];
    				txtEditTelefono.Text = datos[6];
    				txtEditEmail.Text = datos[7];
    				txtEditPassword.Text = datos[9];
    				dlistEditUnidad.ClearSelection();
    				dlistEditUnidad.SelectedValue = datos[4];
    				dListEditTipo.ClearSelection();
    				dListEditTipo.SelectedValue = datos[5];
    				dListEditEstado.ClearSelection();
    				dListEditEstado.SelectedValue = datos[3];
    				dListEditRolesF.ClearSelection();
    				if (datos.Length > 10 && !string.IsNullOrEmpty(datos[10]) && dListEditRolesF.Items.FindByValue(datos[10]) != null)
    				{
    					dListEditRolesF.SelectedValue = datos[10];
    				}
    				mpeEditarRegistro.Show();
    			}
    			if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
    			{
    				GridViewRow gvr2 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
    				string eleccion2 = gvr2.Cells[0].Text.ToString().Trim();
    				Session["idDoc"] = eleccion2;
    				odbusuario = new BaseDatos();
    				string[] datos2 = new string[8];
    				datos2 = odbusuario.datosUsuarios(Convert.ToString(eleccion2));
    				lblDelId.Text = gvr2.Cells[0].Text.ToString().Trim();
    				lblDeleteNombre.Text = gvr2.Cells[1].Text.ToString().Trim();
    				lblDelDirAdm.Text = gvr2.Cells[4].Text.ToString().Trim();
    				mpeEliminarRegistro.Show();
    			}
    		}
    		catch (Exception ex)
    		{
			ErrorHandler.LogError(ex, "Users.GridView1_RowCommand", "editar/eliminar usuario");
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
    		dListEditEstado.ClearSelection();
    		txtEditApellido.Text = string.Empty;
    		txtEditNombre.Text = string.Empty;
    		dListEditEstado.ClearSelection();
    		dListRolesF.ClearSelection();
    		dListEditRolesF.ClearSelection();
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
    			if (!string.IsNullOrEmpty(txtEditId.Text) & !string.IsNullOrEmpty(txtEditPassword.Text) & !string.IsNullOrEmpty(txtEditNombre.Text) & !string.IsNullOrEmpty(txtEditApellido.Text) & (dListEditEstado.SelectedIndex > 0) & (dlistEditUnidad.SelectedIndex > 0) & (dListEditTipo.SelectedIndex > 0))
    			{
    				string id = Convert.ToString(Session["idDoc"]);
    				int? editRolesF = null;
    				if (dListEditRolesF.SelectedIndex > 0)
    				{
    					editRolesF = Convert.ToInt32(dListEditRolesF.SelectedValue);
    				}
    				if (odbusuario.gestionarUsuarios(id, txtEditId.Text, txtEditNombre.Text, txtEditApellido.Text, txtEditPassword.Text, txtEditTelefono.Text, txtEditEmail.Text, Convert.ToInt32(dListEditEstado.SelectedValue), Convert.ToInt32(dlistEditUnidad.SelectedValue), Convert.ToInt32(dListEditTipo.SelectedValue), editRolesF, 4))
    				{
    					ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Edicion de usuario Completado');", addScriptTags: true);
    					limpiarCampos();
    					odbusuario = new BaseDatos();
    					DataSet ds = odbusuario.filtroUsuarios("", "", "", null, null, null);
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
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Debe completar todos los campos &#10;');", addScriptTags: true);
    			}
    		}
    		catch (Exception ex)
    		{
			ErrorHandler.LogError(ex, "Users.btnEditar_Click", "editar usuario");
    			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
    		}
    	}

    	protected void btnEliminar_Click(object sender, EventArgs e)
    	{
    		try
    		{
    			odbusuario = new BaseDatos();
    			string id = Convert.ToString(Session["idDoc"]);
    			if (odbusuario.gestionarUsuarios(id, "", "", "", "", "", "", 0, 0, 0, null, 3))
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Eliminación de usuario Completada');", addScriptTags: true);
    				limpiarCampos();
    				odbusuario = new BaseDatos();
    				DataSet ds = odbusuario.filtroUsuarios("", "", "", null, null, null);
    				if (ds != null)
    				{
    					GridView1.DataSource = ds.Tables[0];
    					GridView1.DataBind();
    					MultiView1.ActiveViewIndex = 0;
    				}
    			}
    			else
    			{
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('No se pudo eliminar el usuario &#10; <br />');", addScriptTags: true);
			}
		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "Users.btnEliminar_Click", "eliminar usuario");
			ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", addScriptTags: true);
		}
    	}
    }

}

