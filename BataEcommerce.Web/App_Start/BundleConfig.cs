using System.Web;
using System.Web.Optimization;

namespace BataEcommerce.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendors/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/validator").Include(
                        "~/Scripts/vendors/validator/validator.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/vendors/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/dist/css/bootstrap.css"));
        }
    }
}
