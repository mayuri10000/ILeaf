using System.Web;
using System.Web.Optimization;

namespace ILeaf.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/main.css",
                      "~/Content/theme.css",
                      "~/Plugins/Font-Awesome/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Global").Include(
                "~/Plugins/jquery-2.0.3.min.js",
                "~/Plugins/bootstrap/js/bootstrap.min.js",
                "~/Plugins/modernizr-2.6.2-respond-1.1.0.min.js"));

            bundles.Add(new StyleBundle("~/Content/Wizard").Include(
                "~/Plugins/jquery-steps-master/demo/css/normalize.css",
                "~/Plugins/jquery-steps-master/demo/css/wizardMain.css",
                "~/Plugins/jquery-steps-master/demo/css/jquery.steps.css"));

            bundles.Add(new ScriptBundle("~/Script/Wizard").Include(
                "~/Plugins/jquery-steps-master/lib/jquery.cookie-1.3.1.js",
                "~/Plugins/jquery-steps-master/build/jquery.steps.js",
                "~/Scripts/WizardInit.js"));
            
        }
    }
}
