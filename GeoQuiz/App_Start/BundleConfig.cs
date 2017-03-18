using System.Web;
using System.Web.Optimization;

namespace GeoQuiz
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            string jqueryCDN = "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js";
            bundles.Add(new ScriptBundle("~/bundles/jquery", jqueryCDN).Include(
                      "~/Scripts/jquery-{version}.js"));

            // Not used, because i use now customized bootstrap
            string bootstrapCDN = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";
            bundles.Add(new StyleBundle("~/Content/css/bootstrap", bootstrapCDN).Include(
                      "~/Content/bootstrap.css"));

            string bootstrapjsCDN = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";
            bundles.Add(new ScriptBundle("~/bundles/bootstrap", bootstrapjsCDN).Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
         //           "~/Scripts/jquery.validate*",
                      "~/Scripts/modernizr-*",
                      "~/Scripts/respond.js",
                      "~/Scripts/progressbar.js",
                      "~/Scripts/material.js",
                      "~/Scripts/ripples.js",
                      "~/Scripts/snackbar.js",
                      "~/Scripts/rating.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-material-design.css",
                      "~/Content/ripples.css",
                      "~/Content/snackbar.css",
                      "~/Content/rating.css",
                      "~/Content/confetti.css",
                      "~/Content/site.css"));
        }
    }
}