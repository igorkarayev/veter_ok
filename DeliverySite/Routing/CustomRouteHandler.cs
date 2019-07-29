using System.Net;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

namespace Delivery.Routing
{
    public class CustomRouteHandler : IRouteHandler
    {
        public CustomRouteHandler(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        public string VirtualPath { get; private set; }

        public IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            if (!UrlAuthorizationModule.CheckUrlAccessForPrincipal(VirtualPath, requestContext.HttpContext.User, requestContext.HttpContext.Request.HttpMethod))
            {
                requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                requestContext.HttpContext.Response.End();
            }

            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            foreach (var urlParm in requestContext.RouteData.Values)
            {
                requestContext.HttpContext.Items[urlParm.Key] = urlParm.Value;
            }
            return page;
        }
    }

}