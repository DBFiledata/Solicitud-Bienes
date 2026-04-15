namespace sitemaCompras.viewsAdmin
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class admin : MasterPage
    {
    	protected ContentPlaceHolder ContentPlaceHolder;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		_ = Page.IsPostBack;
    	}
    }

}

