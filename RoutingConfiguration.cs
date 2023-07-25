namespace webapp
{
    public class RoutingConfiguration
    {
        public static void ConfigureRoutes(WebApplication routes)
        {
            routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
