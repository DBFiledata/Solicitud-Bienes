namespace sitemaCompras
{
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using sitemaCompras.Controlers;

    public partial class login : Page
    {
    	private BaseDatos odbusuario;

    	protected HtmlForm form1;

    	protected Login Login;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!IsPostBack)
    		{
    			ViewState["LoginErrors"] = 0;
    		}
    	}

    	protected void Login_Authenticate1(object sender, AuthenticateEventArgs e)
    	{
    		if (!((Login.Password != string.Empty) & (Login.UserName != string.Empty)))
    		{
    			return;
    		}
    		odbusuario = new BaseDatos();
    		bool loged = false;
    		string[] datos = new string[5];
    		(loged, datos) = odbusuario.login(Login.UserName, Login.Password);
    		if (loged)
    		{
    			Session["id"] = Login.UserName;
    			Session["username"] = datos[0] + " " + datos[1];
    			Session["type"] = datos[2];
    			Session["unidad"] = datos[3];
    			Session["rolesF"] = datos[4];
    			odbusuario.RegistrarAuditoria("LOGIN_OK", "sesion", null, null, null, null, null, "Inicio de sesión", "OK", Login.UserName);
    			FormsAuthentication.SetAuthCookie(Login.UserName, Login.RememberMeSet);
    			FormsAuthentication.RedirectFromLoginPage(Login.UserName, Login.RememberMeSet);
    			string nombre = User.Identity.Name;
    			if ((string)Session["type"] == "1")
    			{
    				Response.Redirect("~/viewsAdmin/index.aspx");
    			}
    			else if ((string)Session["type"] == "2")
    			{
    				Response.Redirect("~/viewsGeneral/index.aspx");
    			}
    			else if ((string)Session["type"] == "3")
    			{
    				Response.Redirect("~/viewsGeneral/index.aspx");
    			}
    			else if ((string)Session["type"] == "4")
    			{
    				Response.Redirect("~/viewsGeneral/index.aspx");
    			}
    			else if ((string)Session["type"] == "5")
    			{
    				Response.Redirect("~/viewsGeneral/index.aspx");
    			}
    			else if ((string)Session["type"] == "6")
    			{
    				Session["unidadEtapa"] = Session["unidad"];
    				switch (Convert.ToInt32(datos[3]))
    				{
    				case 8:
    					Session["unidadEtapa"] = 10;
    					break;
    				case 10:
    					Session["unidadEtapa"] = 11;
    					break;
    				case 11:
    					Session["unidadEtapa"] = 12;
    					break;
    				}
    				Response.Redirect("~/viewsGeneral/index.aspx");
    			}
    			else if ((string)Session["type"] == "7")
    			{
    				Response.Redirect("~/viewsAdmin/index.aspx");
    			}
    			else
    			{
    				ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Su perfil no cuenta con los permisos para acceder a la aplicacion');", addScriptTags: true);
    			}
    		}
    		else
    		{
    			odbusuario.RegistrarAuditoria("LOGIN_FAIL", "sesion", null, null, null, null, null, "Intento de inicio de sesión fallido", "ERROR", Login.UserName);
    			e.Authenticated = false;
    		}
    	}

    	protected void Login_LoginError(object sender, AuthenticateEventArgs e)
    	{
    		if (ViewState["LoginErrors"] == null)
    		{
    			ViewState["LoginErrors"] = 0;
    		}
    		int ErrorCount = (int)ViewState["LoginErrors"] + 1;
    		ViewState["LoginErrors"] = ErrorCount;
    		if (ErrorCount > 3 && Login.PasswordRecoveryUrl != string.Empty)
    		{
    			Response.Redirect(Login.PasswordRecoveryUrl);
    		}
    	}
    }

}

