using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RacingLeagueManager.Data;

namespace RacingLeagueManager
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }
        
        public static WebApplication ConfigureApp(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            return app;
        }
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            IServiceCollection services = builder.Services;

            ConfigureServices(services, connectionString);

            var app = builder.Build();
            ConfigureApp(app);

            //Get service provider
            //var serviceProvider = app.Services.GetService<IServiceProvider>();

            //if (serviceProvider != null)
            //    CreateRole(serviceProvider).Wait();

            app.Run();
        }

        public static async Task CreateRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var adminRoleExists = await roleManager.RoleExistsAsync("admin");

            if (adminRoleExists == false)
            {
                await roleManager.CreateAsync(new IdentityRole("admin")); 
            }
        }
    }
}
