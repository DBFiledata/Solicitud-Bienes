namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Web.Security;
    using System.Web.UI;

    public partial class index : Page
    {
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
    		else if (!((Convert.ToInt32(Session["type"]) == 1) | (Convert.ToInt32(Session["type"]) == 7)))
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    	}
    }

}

