using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Optimization;

namespace Hys.CareRIS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/json3.min.js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.signalR-{version}.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-route.js")
                .Include("~/Scripts/angular-translate.js")
                .Include("~/Scripts/angular-translate-loader-partial.js")
                .Include("~/Scripts/angular-route.js")
                .Include("~/Scripts/angular-cookies.js")
                .Include("~/Scripts/angular-sanitize.js")
                .Include("~/Scripts/angular-messages.js")
                .Include("~/Scripts/angular-animate.js")
                .Include("~/Scripts/angular-idle.js")
                .Include("~/Scripts/angular-upload.min.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/angular-ui/ui-utils-ieshiv.js")
                .Include("~/Scripts/angular-ui/ui-utils.js")
                .Include("~/Scripts/angular-ui-router.js")
                .Include("~/Scripts/ct-ui-router-extras.js")
                .Include("~/Scripts/ng-grid.js")
                .Include("~/Scripts/webcam.js")
                .Include("~/Scripts/underscore.js")
                .Include("~/Scripts/select2.js")
                .Include("~/Scripts/select.js")
                .Include("~/Scripts/jquery.floatThead.js")
                .Include("~/Scripts/kendo-partial/kendo.js")
                .Include("~/Scripts/coordiante.js")
                .Include("~/Scripts/fabric.min.js")
                );

            bundles.Add(new StyleBundle("~/bundles/css")
                    .Include("~/Content/bootstrap.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/bootstrap-theme.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/site.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/css/select2.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/animate.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/ng-grid.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/select.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.common.core.min.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.bootstrap.min.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.common-bootstrap.min.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.default.min.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.dataviz.min.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/kendo/kendo.dataviz.default.min.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new StyleBundle("~/bundles/login-css")
                    .Include("~/Content/bootstrap.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/bootstrap-theme.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/site.css", new CssRewriteUrlTransformFixed())
                    .Include("~/Content/animate.css", new CssRewriteUrlTransformFixed())
                    .Include("~/app-resources/css/site.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new ScriptBundle("~/bundles/login-js").Include(
                "~/Scripts/json3.min.js",
                "~/Scripts/es6-promise.auto.min.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/axios.min.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/js-cookie/js.cookie.js",
                "~/Scripts/login.js"));

            RegisterAppBundles(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }

        public static void RegisterAppBundles(BundleCollection bundles)
        {
            var compilationSection = (CompilationSection)System.Configuration.ConfigurationManager.GetSection(@"system.web/compilation");
            var isEnableDebug = compilationSection.Debug;

            // TODO: register your module bundles
            // scripts loaded at first time
            bundles.Add(CreateScriptBundle("~/bundles/app-js", isEnableDebug)
                .IncludeSubDir("~/app", "*.js")
                );

            bundles.Add(new StyleBundle("~/bundles/app-css")
                .Include("~/app-resources/css/site.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/worklist.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/registration.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/exam-module-view.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/exam-add-item.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/exam-viewer-view.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/exam-info-view.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/time-slice-view.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/report.css", new CssRewriteUrlTransformFixed())
                .Include("~/app-resources/css/report-edit-view.css", new CssRewriteUrlTransformFixed())
                .IncludeSubDir("~/app", "*.css")
                );
        }

        // bundle only if enable debug, otherwise minify the js files
        private static Bundle CreateScriptBundle(string virtualPath, bool isEnableDebug)
        {
            // if you want to disable the minification, change the ScriptBundle to Bundle
            return isEnableDebug ? new Bundle(virtualPath) : new ScriptBundle(virtualPath);
        }
    }
    public static class BundleExtensions
    {
        public static Bundle IncludeSubDir(this Bundle bundle, string path, string searchPattern)
        {
            var isStyleBundle = bundle is StyleBundle;


            if (isStyleBundle)
            {
                var rootPath = HttpContext.Current.Server.MapPath("~/");
                var absolutePath = HttpContext.Current.Server.MapPath(path);
                var directoryInfo = new DirectoryInfo(absolutePath);

                var files = directoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories);

                foreach (var file in files)
                {

                    bundle.Include(file.FullName.Replace(rootPath, "~/").Replace('\\', '/'), new CssRewriteUrlTransformFixed());
                }
            }
            else
            {
                bundle.IncludeDirectory(path, searchPattern, true);
            }

            return bundle;
        }
    }
}
