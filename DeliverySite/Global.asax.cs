using Delivery.DAL;
using DeliverySite.Modules;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Delivery
{
    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //загружаем роли в оперативную память
            var roles = new DAL.DataBaseObjects.Roles();
            Application["RolesList"] = roles.GetAllItemsToList();

            //загружаем backend в оперативную память
            var districts = new DAL.DataBaseObjects.Districts();
            Application["Districts"] = districts.GetAllItemsToList();

            //загружаем города в оперативную память
            var city = new DAL.DataBaseObjects.City();
            Application["CityList"] = city.GetAllItemsToList();

            //загружаем backend в оперативную память
            var backend = new DAL.DataBaseObjects.Backend();
            Application["BackendList"] = backend.GetAllItemsToList();

            new TicketStatisticTimerModule().Init();
        }

        protected virtual void Session_Start(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(Object sender, EventArgs e)
        {
            //если список ролей не загружен в память - загружаем его
            if (Application["RolesList"] == null)
            {
                var roles = new DAL.DataBaseObjects.Roles();
                Application["RolesList"] = roles.GetAllItemsToList();
            }
        }

        protected virtual void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_Error(Object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var ex = context.Server.GetLastError();
            //if (ex.GetType() == typeof (HttpException))
            var ip = HttpContext.Current.Request.UserHostAddress;
            LogErrors.LogedError(ex, ip);
        }

        protected virtual void Session_End(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_End(Object sender, EventArgs e)
        {
        }
    }
}

