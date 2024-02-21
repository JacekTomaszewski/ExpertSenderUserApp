using ExpertSenderUserApp.Web.Enums;
using Microsoft.Data.SqlClient;

namespace ExpertSenderUserApp.Web.Routes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(WebApplication app)
        {
            app.MapControllerRoute(
                name: "Index",
                pattern: "{controller=User}/{action=Index}");

            app.MapControllerRoute(
                name: "Create",
                pattern: "User/Create",
                defaults: new { controller = "User", action = "CreateViewEdit", userModeEnum = UserModeEnum.Create });

            app.MapControllerRoute(
                name: "View",
                pattern: "User/View/{id}",
                defaults: new { controller = "User", action = "CreateViewEdit", userModeEnum = UserModeEnum.View });

            app.MapControllerRoute(
                name: "Edit",
                pattern: "User/Edit/{id}",
                defaults: new { controller = "User", action = "CreateViewEdit", userModeEnum = UserModeEnum.Edit });
        }
    }
}
