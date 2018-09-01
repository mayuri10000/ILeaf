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

            bundles.Add(new StyleBundle("~/Content/Global").Include(
                      "~/Plugins/bootstrap/css/bootstrap.css",
                      "~/Content/main.css",
                      "~/Content/theme.css",
                      "~/Plugins/Font-Awesome/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Global").Include(
                "~/Plugins/jquery-2.0.3.min.js",
                "~/Plugins/bootstrap/js/bootstrap.min.js",
                "~/Plugins/modernizr-2.6.2-respond-1.1.0.min.js",
                "~/Plugins/gritter/js/jquery.gritter.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Calendar").Include(
                "~/Scripts/jquery-ui.min.js",
                "~/Plugins/uniform/jquery.uniform.min.js",
                "~/Plugins/inputlimiter/jquery.inputlimiter.1.3.1.min.js",
                "~/Plugins/datepicker/js/bootstrap-datepicker.js",
                "~/Plugins/timepicker/js/bootstrap-timepicker.min.js",
                "~/Plugins/fullcalendar-3.9.0/lib/moment.min.js",
                "~/Plugins/fullcalendar-3.9.0/fullcalendar.js",
                "~/Plugins/fullcalendar-3.9.0/locale/zh-cn.js",
                "~/Scripts/ileaf.calendar.js",
                "~/Scripts/jquery.form.js"));

            bundles.Add(new StyleBundle("~/Content/Calendar").Include(
                "~/Plugins/fullcalendar-3.9.0/fullcalendar.css",
                "~/Plugins/datepicker/css/datepicker.css",
                "~/Plugins/timepicker/css/bootstrap-timepicker.min.css"));
        }
    }
}
