namespace sitemaCompras
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class _Default : Page
    {
    	protected HtmlForm form1;

    	protected Label Label1;

    	protected Button Button1;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    	}

    	protected void Button1_Click(object sender, EventArgs e)
    	{
    		Response.Redirect("login.aspx");
    	}
    }

}

