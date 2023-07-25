using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using webapp.Controllers;

namespace webapp
{
    public class RoutingConfiguration
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map routes for AdminController
            app.MapControllerRoute(
                name: "admin",
                pattern: "Admin/{action=Index}/{id?}",
                defaults: new { controller = "Admin" }); // Specify the controller name as a string here
        }
    }
}
