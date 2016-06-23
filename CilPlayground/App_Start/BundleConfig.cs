using System.Web;
using System.Web.Optimization;

namespace CilPlayground
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //Project  js bundles
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/scripts/Cili/changeElementType.js",
                "~/scripts/Cili/admin.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/examples").Include(
               "~/scripts/Cili/codeBox.js",
               "~/scripts/Cili/examples.js",
               "~/scripts/Cili/blockInput.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
               "~/scripts/Cili/home.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/instructions").Include(
               "~/scripts/Cili/changeElementType.js",
               "~/scripts/Cili/instructions.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/interpreter").Include(
               "~/scripts/Cili/codeBox.js",
               "~/scripts/Cili/interpreter.js",
               "~/scripts/Cili/interpreterLogic.js",
               "~/scripts/Cili/blockInput.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/rename").Include(
               "~/scripts/Cili/rename.js",
               "~/scripts/Cili/blockInput.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/saveAs").Include(
               "~/scripts/Cili/saveAs.js",
               "~/scripts/Cili/blockInput.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/signIn").Include(
               "~/scripts/Cili/modal.signin.js",
               "~/scripts/Cili/blockInput.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/signUp").Include(
               "~/scripts/Cili/modal.signup.js",
               "~/scripts/Cili/blockInput.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/Layout").Include(
               "~/scripts/Cili/signOut.js",
               "~/scripts/Cili/Sha512.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/try").Include(
               "~/scripts/Cili/interpreterLogic.js",
               "~/scripts/Cili/codeBox.js",
               "~/scripts/Cili/blockInput.js",
               "~/scripts/Cili/try.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/userCodes").Include(
               "~/scripts/Cili/userCodes.js",
               "~/scripts/Cili/codeBox.js",
               "~/scripts/Cili/blockInput.js"
               ));
        }
    }
}
