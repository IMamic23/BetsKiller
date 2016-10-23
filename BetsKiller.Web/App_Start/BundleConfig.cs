using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace BetsKiller.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/preloader.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/wysihtml5-{version}.js",
                        "~/Scripts/bootstrap3-wysihtml5.js",
                        "~/Scripts/dataTables.bootstrap.min.js",
                        "~/Scripts/bootstrap-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard-js").Include(
                        "~/Scripts/Dashboard/jquery-ui.min.js",
                        "~/Scripts/Dashboard/raphael.min.js",
                        "~/Scripts/Dashboard/morris.min.js",
                        "~/Scripts/Dashboard/fastclick.min.js",
                        "~/Scripts/Dashboard/app.js",
                        "~/Scripts/Dashboard/custom.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/landing-js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/preloader.js",
                        "~/Scripts/Landing/wow.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/Landing/jquery.superslides.min.js",
                        "~/Scripts/Landing/slick.min.js",
                        "~/Scripts/Landing/jquery.circliful.min.js",
                        "~/Scripts/Landing/custom.js",
                        "~/Scripts/bootstrap-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/logsignin-js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/preloader.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard-betstracker").Include(
                        "~/Scripts/Dashboard/jquery.cycle2.js",
                        "~/Scripts/Dashboard/jquery.simplePagination.js",
                        "~/Scripts/Dashboard/jquery.sparkline.min.js",
                        "~/Scripts/Dashboard/Chart.js"
                        ));

            #endregion

            #region Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/preloader.css",
                      "~/Content/bootstrap-wysihtml5.css",
                      "~/Content/bootstrap3-wysiwyg5-color.css",
                      "~/Content/dataTables.bootstrap.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/dashboard-css").Include(
                      "~/Content/Dashboard/morris.css",
                      "~/Content/Dashboard/AdminLTE.css",
                      "~/Content/Dashboard/skin-blue.min.css",
                      "~/Content/Dashboard/Custom.css",
                      "~/Content/animate.css"
            ));

            bundles.Add(new StyleBundle("~/Content/landing-css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/preloader.css",
                      "~/Content/Landing/superslides.css",
                      "~/Content/Landing/slick.css",
                      "~/Content/animate.css",
                      "~/Content/Landing/elastic_grid.css",
                      "~/Content/Landing/jquery.circliful.css",
                      "~/Content/Landing/default-theme.css",
                      "~/Content/Landing/style.css"
            ));

            bundles.Add(new StyleBundle("~/Content/logsignin-css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/preloader.css",
                      "~/Content/LogSignIn/AdminLTE.min.css",
                      "~/Content/LogSignIn/custom.css"
            ));

            #endregion

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = bool.Parse(ConfigurationManager.AppSettings["EnableOptimizations"].ToString());
        }
    }
}
