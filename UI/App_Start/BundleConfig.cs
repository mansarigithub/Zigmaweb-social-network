using System.Web.Optimization;

namespace ZigmaWeb.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/visitor-layout-styles").Include("~/content/assets/global/plugins/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/global/plugins/simple-line-icons/simple-line-icons.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/global/plugins/bootstrap/css/bootstrap-rtl.min.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/global/css/components-md-rtl.opt.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/layouts/layout3/css/layout-rtl.min.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/layouts/layout3/css/themes/default-rtl.min.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/layouts/layout3/css/custom-rtl.min.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/global/css/zigmaweb.css", new CssRewriteUrlTransform())
                .Include("~/content/assets/global/css/visitor_layout.css", new CssRewriteUrlTransform())
                );

            bundles.Add(new StyleBundle("~/visitor-article-styles").Include("~/Content/assets/pages/css/visitor_article_index.css", new CssRewriteUrlTransform())
          .Include("~/Content/assets/global/plugins/bootstrap-select/css/bootstrap-select-rtl.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/visitor-blog-index-style").Include("~/Content/assets/pages/css/visitor_blog_index.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/visitor-blog-post-styles").Include("~/Content/assets/pages/css/visitor_article_index.css", new CssRewriteUrlTransform())
                .Include("~/Content/assets/global/plugins/bootstrap-select/css/bootstrap-select-rtl.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/visitor-layout-scripts").Include("~/content/assets/global/plugins/jquery.min.js")
                .Include("~/content/assets/global/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js")
                .Include("~/content/assets/global/scripts/app.min.js")
                .Include("~/content/assets/layouts/layout3/scripts/layout.min.js")
                .Include("~/content/assets/global/plugins/persian-number/persianumber.min.js")
                .Include("~/content/assets/global/scripts/zigmaweb.js"));

            bundles.Add(new ScriptBundle("~/visitor-layout-scripts-signalR").Include("~/content/assets/global/plugins/jquery.min.js")
               .Include("~/Scripts/jquery.signalR-2.2.1.min.js")
               .Include("~/signalr/hubs")
              .Include("~/content/assets/global/plugins/bootstrap/js/bootstrap.min.js")
              .Include("~/content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js")
              .Include("~/content/assets/global/scripts/app.min.js")
              .Include("~/content/assets/layouts/layout3/scripts/layout.min.js")
              .Include("~/content/assets/global/plugins/persian-number/persianumber.min.js")
              .Include("~/content/assets/global/scripts/zigmaweb.js"));

            bundles.Add(new ScriptBundle("~/signalR-scripts").Include("~/Scripts/jquery.signalR-2.2.1.min.js")
                .Include("~/signalr/hubs"));

            bundles.Add(new ScriptBundle("~/visitor-article-scripts").Include("~/Content/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js")
               .Include("~/Content/assets/pages/scripts/visitor_article_index.js")
               .Include("~/Content/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js"));

            bundles.Add(new ScriptBundle("~/visitor-blog-scripts").Include("~/Content/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js")
            .Include("~/Content/assets/pages/scripts/visitor_blog_post.js"));
            //bundles.Add(new ScriptBundle("~/bundles/VisitorJs").Include(
            //            "~/Scripts/jquery-{version}.js",
            //            "~/Scripts/jquery.unobtrusive-ajax.min.js"
            //            ));

            //bundles.Add(new StyleBundle("~/Content/VisitorCss").Include(
            //          "~/content/css/visitor_layout.css"));


            //bundles.Add(new StyleBundle("~/Content/UserCss").Include(
            //    "~/content/css/user_layout.css"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = false;
        }
    }
}
