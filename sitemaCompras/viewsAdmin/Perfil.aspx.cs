namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using sitemaCompras.Controlers;

    public partial class Perfil : Page
    {
    	private BaseDatos odbusuario;

    	protected ScriptManager ScriptManager1;

    	protected UpdatePanel UpdatePanel1;

    	protected Label Label7;

    	protected TextBox txtId;

    	protected Label Label1;

    	protected TextBox txtNombre;

    	protected Label Label2;

    	protected TextBox txtApellido;

    	protected Label Label9;

    	protected TextBox txtTelefono;

    	protected Label Label3;

    	protected TextBox txtEmail;

    	protected Label Label4;

    	protected DropDownList listUnidad;

    	protected Label Label6;

    	protected DropDownList listTipo;

    	protected Button btnRegistrar;

    	protected Button btnCancelar;

    	protected Label lblerror;

    	protected RequiredFieldValidator RequiredFieldValidatorId;

    	protected RegularExpressionValidator RegularExpressionValidatorId;

    	protected RequiredFieldValidator RequiredFieldValidatorIdNombre;

    	protected RequiredFieldValidator RequiredFieldValidatorApellido;

    	protected RequiredFieldValidator RequiredFieldValidatorTelefono;

    	protected RegularExpressionValidator RegularExpressionValidatorTelefono;

    	protected RequiredFieldValidator RequiredFieldValidatorEmail;

    	protected RegularExpressionValidator RegularExpressionValidatorEmail;

    	protected RequiredFieldValidator RequiredFieldValidatorListTipo;

    	protected ValidationSummary ValidationSummary;

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
    		else if ((Convert.ToInt32(Session["type"]) == 1) | (Convert.ToInt32(Session["type"]) == 7))
    		{
    			if (!Page.IsPostBack)
    			{
    				odbusuario = new BaseDatos();
    				string[] datos = new string[6];
    				datos = odbusuario.datosUsuarios((string)Session["id"]);
    				txtId.Text = datos[0];
    				txtNombre.Text = datos[1];
    				txtApellido.Text = datos[2];
    				txtEmail.Text = datos[0];
    				txtTelefono.Text = datos[0];
    				Session["infoUserEstatus"] = datos[3];
    				odbusuario = new BaseDatos();
    				DataSet ds1 = odbusuario.dListaTipoUsuario();
    				listTipo.Items.Clear();
    				listTipo.DataSource = ds1;
    				listTipo.DataBind();
    				listTipo.Items.Insert(0, "\ud83d\udc65 Seleccionar");
    				listTipo.SelectedValue = datos[5];
    				odbusuario = new BaseDatos();
    				DataSet ds2 = odbusuario.dListaUnidadesSolicitantes();
    				listUnidad.Items.Clear();
    				listUnidad.DataSource = ds2;
    				listUnidad.DataBind();
    				listUnidad.Items.Insert(0, "\ud83c\udfdb\ufe0f Seleccionar");
    				listUnidad.SelectedValue = datos[4];
    				if (Convert.ToInt32(Session["type"]) == 7)
    				{
    					listTipo.Visible = false;
    					listUnidad.Visible = false;
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
    		try
    		{
    			lblerror.Visible = false;
    			odbusuario = new BaseDatos();
    			if (odbusuario.gestionarUsuarios((string)Session["id"], txtId.Text, txtNombre.Text, txtApellido.Text, "", txtTelefono.Text, txtEmail.Text, (int)Session["infoUserEstatus"], Convert.ToInt32(txtTelefono.Text), Convert.ToInt32(listTipo.Text), 2))
    			{
    				Session["id"] = txtId.Text;
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Su perfil a sido actualizado satisfactoriamente');", addScriptTags: true);
    			}
    			else
    			{
    				lblerror.Text = "No se pudo actualizar su informacion <br />-verifique si la informacion esta ingresada correctamente <br />-verifique que al id que intenta registrar no este creado";
    				lblerror.Visible = true;
    			}
    		}
		catch (Exception ex)
		{
			ErrorHandler.LogError(ex, "Perfil.btnRegistrar_Click", "editar perfil usuario");
			lblerror.Text = " Error No se pudo editar su informacion<br />-verifique si la informacion esta ingresada correctamente <br />-verifique que al id que intenta utilizar no este creado";
			lblerror.Visible = true;
		}
    	}

    	protected void btnCancelar_Click(object sender, EventArgs e)
    	{
    		Response.Redirect("inicioAdmin.aspx");
    	}
    }

}

