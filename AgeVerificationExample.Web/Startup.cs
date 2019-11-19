using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AgeVerificationExample.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Contracts;
using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AgeVerificationExample.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services">The application service collection to be configured</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationUserIdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                    // Implement required password policy
                    options.Password =
                        new PasswordOptions
                        {
                            RequiredLength = 12,
                            RequireDigit = true,
                            RequireUppercase = true,
                            RequireLowercase = true,
                            RequireNonAlphanumeric = true
                        };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddApplicationUserContext();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc()
                .AddSessionStateTempDataProvider();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The web hosting environment</param>
        #pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        #pragma warning restore CA1822 // Mark members as static
        {
            Contract.Requires(app != null);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Apply database migrations to ensure database is up to date when app starts
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var database = serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database;
                database.Migrate();
            }
        }
    }
}
