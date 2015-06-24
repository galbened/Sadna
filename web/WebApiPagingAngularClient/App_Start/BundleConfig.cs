using System.Web;
using System.Web.Optimization;

namespace WebApiPagingAngularClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/angular.js",
                    "~/Scripts/ui-bootstrap.js",
                    "~/Scripts/ui-bootstrap-tpls.js",
                    "~/Scripts/angular-resource.js",
                    "~/Scripts/angular-route.js",
                    "~/app/js/app.js",
                    "~/app/js/ForumListCtrl.js",
                    "~/app/js/ThreadListCtrl.js",
                    "~/app/js/SubForumListCtrl.js",
                    "~/app/js/LoginModalCtrl.js",
                    "~/app/js/SignupModalCtrl.js",
                    "~/app/js/AddForumModalCtrl.js",
                    "~/app/js/RemoveForumCtrl.js",
                    "~/app/js/AddSubForumModalCtrl.js",
                    "~/app/js/RemoveSubForumCtrl.js",
                    "~/app/js/AddAdminModalCtrl.js",
                    "~/app/js/AddModeratorModalCtrl.js",
                    "~/app/js/SetPolicyModalCtrl.js",
                    "~/app/js/ComplainModertorModalCtrl.js",
                    "~/app/js/Api.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;

        }
    }
}
