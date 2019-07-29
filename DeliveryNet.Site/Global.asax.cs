using DeliveryNet.Data.Context;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Delivery
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MefConfig.RegisterMef();
            /* отключение проверки соответствия версии базы данных миграциям */
            Database.SetInitializer<DeliveryNetContext>(null);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var ci = new CultureInfo("ru");
            //Finally setting culture for each request
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
    }
}
