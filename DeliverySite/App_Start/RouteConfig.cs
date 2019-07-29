using Delivery.Routing;
using System.Web.Routing;

namespace Delivery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = false;

            #region General routes

            routes.Add("Default", new Route("default", new CustomRouteHandler("~/Logon.aspx")));
            routes.Add("UserNotification", new Route("usernotification/{id}/{username}", new CustomRouteHandler("~/UserNotification.aspx")));
            routes.Add("UserNotificationWithoutUser", new Route("usernotification/{id}", new CustomRouteHandler("~/UserNotification.aspx")));
            routes.Add("UserNotificationWithoutParametr", new Route("usernotification", new CustomRouteHandler("~/UserNotification.aspx")));
            routes.Add("ForgotPassword", new Route("forgotpassword", new CustomRouteHandler("~/ForgotPassword.aspx")));
            routes.Add("ChangePassword", new Route("changepassword/{sacredlink}", new CustomRouteHandler("~/ChangePassword.aspx")));

            #endregion

            routes.Add(new Route("*\\.json", new StopRoutingHandler()));
        }
    }
}
