using System.Web.Optimization;

namespace Delivery
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        "~/Scripts/jquery-1.8.3.js",
                        "~/Scripts/CustomScripts/bbcode-settings.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-ui").Include(
                       "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-add").Include(
                        "~/Scripts/jquery.autocomplete.js",
                        "~/Scripts/jquery.maskedinput.js",
                        "~/Scripts/jquery.maskmoney.js",
                        "~/Scripts/jquery.timepicker.js",
                        "~/Scripts/jquery.markitup.js",
                        "~/Scripts/jqueru.datapicker.lang.ru.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-alerts").Include(
                        "~/Scripts/jquery.alerts.js"));

            bundles.Add(new ScriptBundle("~/js/custom-scripts").Include(
                        "~/Scripts/CustomScripts/global-functions.js",
                        "~/Scripts/CustomScripts/global-validators.js"));



            bundles.Add(new StyleBundle("~/styles/jquery").Include(
                        "~/Styles/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/styles/jquery-add").Include(
                        "~/Styles/jquery.timepicker.css",
                        "~/Styles/bbcode-style.css"));

            bundles.Add(new StyleBundle("~/styles/user").Include(
                        "~/Styles/user.css"));

            bundles.Add(new StyleBundle("~/styles/manager").Include(
                        "~/Styles/manager.css"));

            bundles.Add(new StyleBundle("~/styles/empty").Include(
                        "~/Styles/empty.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}