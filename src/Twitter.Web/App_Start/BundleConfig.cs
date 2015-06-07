using System.Web.Optimization;

namespace Twitter.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/vendor/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/backbone")
                .Include("~/Scripts/vendor/underscore*")
                .Include("~/Scripts/vendor/backbone*"));

            bundles.Add(new ScriptBundle("~/bundles/signalr")
                .Include("~/Scripts/vendor/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/vendor/bootstrap.js")
                .Include("~/Scripts/vendor/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/models", "*.js", false)
                .IncludeDirectory("~/Scripts/collections", "*.js", false)
                .IncludeDirectory("~/Scripts/views", "*.js", false)
                .IncludeDirectory("~/Scripts/routers", "*.js", false)
                .Include("~/Scripts/app.js")
                );

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/site.css"));
        }
    }
}
