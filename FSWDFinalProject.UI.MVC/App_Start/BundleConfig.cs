using System.Web.Optimization;

namespace FSWDFinalProject.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            //JS Files go here
            bundles.Add(new ScriptBundle("~/bundles/template").Include(
             "~/Content/assets/vendor/jquery/jquery.min.js",
             "~/Content/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
             "~/Content/assets/vendor/jquery.easing/jquery.easing.min.js",
             "~/Content/assets/vendor/php-email-form/validate.js",
             "~/Content/assets/vendor/isotope-layout/isotope.pkgd.min.js",
             "~/Content/assets/vendor/venobox/venobox.min.js",
             "~/Content/assets/vendor/waypoints/jquery.waypoints.min.js",
             "~/Content/assets/vendor/owl.carousel/owl.carousel.min.js",
             "~/Scripts/DataTables/jquery.datatables.min.js",
             "~/Content/assets/js/main.js"
             ));


            //CSS files go here
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/vendor/icofont/icofont.min.css",
                      "~/Content/DataTables/css/jquery.datatables.min.css",
                      "~/Content/assets/vendor/boxicons/css/boxicons.min.css",
                      "~/Content/assets/vendor/animate.css/animate.min.css",
                      "~/Content/assets/vendor/remixicon/remixicon.css",
                      "~/Content/assets/vendor/venobox/venobox.css",
                      "~/Content/assets/vendor/owl.carousel/assets/owl.carousel.min.css",
                      "~/Content/assets/css/style.css"
                      ));

           

            //js  
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-{version}.js"));
            //css  
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/jquery-ui.css"));
        }
    }
}
