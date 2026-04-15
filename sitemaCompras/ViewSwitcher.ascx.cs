namespace sitemaCompras
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.UI;
    using Microsoft.AspNet.FriendlyUrls.Resolvers;

    public partial class ViewSwitcher : UserControl
    {
    	protected string CurrentView { get; private set; }

    	protected string AlternateView { get; private set; }

    	protected string SwitchUrl { get; private set; }

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		bool isMobile = WebFormsFriendlyUrlResolver.IsMobileView(new HttpContextWrapper(Context));
    		CurrentView = (isMobile ? "Mobile" : "Desktop");
    		AlternateView = (isMobile ? "Desktop" : "Mobile");
    		string switchViewRouteName = "AspNet.FriendlyUrls.SwitchView";
    		RouteBase switchViewRoute = RouteTable.Routes[switchViewRouteName];
    		if (switchViewRoute == null)
    		{
    			Visible = false;
    			return;
    		}
    		string url = GetRouteUrl(switchViewRouteName, new
    		{
    			view = AlternateView,
    			__FriendlyUrls_SwitchViews = true
    		});
    		url = url + "?ReturnUrl=" + HttpUtility.UrlEncode(Request.RawUrl);
    		SwitchUrl = url;
    	}
    }

}

