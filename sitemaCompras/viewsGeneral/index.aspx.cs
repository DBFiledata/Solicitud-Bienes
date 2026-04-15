namespace sitemaCompras.viewsGeneral
{
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class index : Page
    {
    	protected Image imgIndex;

    	private void Page_PreInit(object sender, EventArgs e)
    	{
    		if ((string)Session["type"] == "2")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterSolicitantes.master";
    		}
    		else if ((string)Session["type"] == "3")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterAprobador.master";
    		}
    		else if ((string)Session["type"] == "4")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterRecepcionCompras.master";
    		}
    		else if ((string)Session["type"] == "5")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterCotizador.master";
    		}
    		else if ((string)Session["type"] == "6")
    		{
    			MasterPageFile = "~/viewsGeneral/NestedMasterOtrasRecepciones.master";
    		}
    	}

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		if (!User.Identity.IsAuthenticated)
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/Login.aspx");
    		}
    		else if (((string)Session["type"] != "2") & ((string)Session["type"] != "3") & ((string)Session["type"] != "4") & ((string)Session["type"] != "5") & ((string)Session["type"] != "6"))
    		{
    			Session.Abandon();
    			Session.Clear();
    			FormsAuthentication.SignOut();
    			Response.Redirect("~/login.aspx");
    		}
    		if ((string)Session["type"] == "2")
    		{
    			imgIndex.ImageUrl = "../userscontent/img/gemeralImg.png";
    		}
    		else if ((string)Session["type"] == "3")
    		{
    			imgIndex.ImageUrl = "../userscontent/img/aproved.png";
    		}
    		else if ((string)Session["type"] == "4")
    		{
    			imgIndex.ImageUrl = "../userscontent/img/recepcionImg.png";
    		}
    		else if ((string)Session["type"] == "5")
    		{
    			imgIndex.ImageUrl = "../userscontent/img/cotizadorImg.png";
    		}
    		else if ((string)Session["type"] == "6")
    		{
    			imgIndex.ImageUrl = "../userscontent/img/recepcionImg.png";
    		}
    	}
    }

}

