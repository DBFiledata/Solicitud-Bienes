namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class adminUsers : MasterPage
    {
    	protected Image Image2;

    	protected ContentPlaceHolder contenido;

    	protected Button btnRegbtnCerrarsesionistrar;

    	protected Image Image3;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		_ = Page.IsPostBack;
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

