namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class NestedMasterOtrasRecepciones : MasterPage
    {
    	protected Image Image2;

    	protected Label lblNombreUser;

    	protected ContentPlaceHolder contenido;

    	protected Button btnRegbtnCerrarsesionistrar;

    	protected Image Image3;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (Page.IsPostBack)
    		{
    			return;
    		}
    		try
    		{
    			if (contenido.Page != null)
    			{
    				string page = contenido.Page.GetType().Name;
    				if (page != "viewsgeneral_index_aspx")
    				{
    					lblNombreUser.Text = " " + Convert.ToString(Session["username"]) + " ";
    				}
    				else
    				{
    					lblNombreUser.Text = "Bienvenido " + Convert.ToString(Session["username"]) + " ";
    				}
    			}
    		}
    		catch (Exception)
    		{
    		}
    	}

    	protected void btnCerrarsesion_Click(object sender, EventArgs e)
    	{
    		Session.Abandon();
    		Session.Clear();
    		FormsAuthentication.SignOut();
    		Response.Redirect("~/login.aspx");
    	}
    }

}

