using System.Web;
using System.Web.Optimization;

namespace GeoQuiz
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/modernizr-*",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/progressbar.js",
                      "~/Scripts/material.js",
                      "~/Scripts/ripples.js",
                      "~/Scripts/snackbar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-material-design.css",
                      "~/Content/ripples.css",
                      "~/Content/snackbar.css"));
        }
    }
}