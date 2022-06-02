using Microsoft.EntityFrameworkCore;
using Footballer_Catalog.Models;
using Footballer_Catalog.Services;
using Footballer_Catalog.Hubs;
using Footballer_Catalog.Db;
using Footballer_Catalog.Repositories;

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
        services.AddTransient<IFootballersService, FootballersService>();
        services.AddScoped<IRepository<Footballer>, FootballerRepository>();
        services.AddScoped<IRepository<Team>, TeamRepository>();
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