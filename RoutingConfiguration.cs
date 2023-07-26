using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using webapp.Controllers;
using webapp.Models;

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
                defaults: new { controller = "Admin" });
            
            app.MapControllerRoute(
                name: "viewCart",
                pattern: "Home/ViewCart",
                defaults: new { controller = "Home", action = "ViewCart" });
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllersWithViews();

            // Register the AppDbContext with the dependency injection container and pass the configuration options
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });

            // Register IAppDbContext with the AppDbContext implementation
            services.AddScoped<IAppDbContext, AppDbContext>();

            // Add session services
            services.AddSession(options =>
            {
                // Set a timeout for the session
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

    }
}
