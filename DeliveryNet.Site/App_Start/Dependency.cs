using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Delivery
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            var assemblyCatalog = new AggregateCatalog();
            assemblyCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            assemblyCatalog.Catalogs.Add(new DirectoryCatalog("bin"));
            var container = new CompositionContainer(assemblyCatalog);

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactoryWithMef(container));
        }
    }

    public class ControllerFactoryWithMef : DefaultControllerFactory
    {
        private readonly CompositionContainer container;
        public ControllerFactoryWithMef(CompositionContainer container)
        {
            this.container = container;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    throw new HttpException(404, "Not found");
                Lazy<object, object> export = container.GetExports(controllerType, null, null).FirstOrDefault();
                IController result;
                if (export == null)
                {
                    result = base.GetControllerInstance(requestContext, controllerType);
                    container.ComposeParts(result);
                }
                else
                {
                    result = (IController) export.Value;

                }
                return result;
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
                throw;
            }
            catch (CompositionException ex)
            {
                throw ex;
            }
        }

        public override void ReleaseController(IController controller)
        {
            ((IDisposable)controller).Dispose();
        }
    }
}