using LowercaseRoutesMVC;
using System.Web.Mvc;
using System.Web.Routing;

namespace Delivery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase("Contacts", "contacts", new { controller = "Home", action = "Contacts" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase("Vacancies", "vacancies", new { controller = "Home", action = "Vacancies" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase("City", "city", new { controller = "Home", action = "City" }, new[] { "Delivery.Controllers" });
            //routes.MapRouteLowercase("News", "news", new { controller = "News", action = "Stream" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase("AboutUs", "aboutus", new { controller = "Home", action = "AboutUs" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase("News details", "news/{titleUrl}", new { controller = "News", action = "Details" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase("News", "news", new { controller = "Home", action = "News" }, new[] { "Delivery.Controllers" });
            routes.MapRouteLowercase(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
