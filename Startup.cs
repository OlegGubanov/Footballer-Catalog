using Microsoft.EntityFrameworkCore;
using Footballer_Catalog.Models;

namespace Footballer_Catalog;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<FootballerContext>(options => options.UseSqlServer(connection));
        services.AddControllersWithViews();
        services.AddSignalR();
    }
 
    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
             
        app.UseStaticFiles();
 
        app.UseRouting();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<FootballersHub>("/update");
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}