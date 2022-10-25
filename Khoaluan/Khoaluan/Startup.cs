using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace Khoaluan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GameStoreDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("conString"));
            });
            services.AddSession();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IGameStoreRepository<>), typeof(GameStoreRepository<>));
            var allRepositoryInterfaces = Assembly.GetAssembly(typeof(IGameStoreRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
            var allRepositoryImplements = Assembly.GetAssembly(typeof(GameStoreRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
            foreach (var repoType in allRepositoryInterfaces.Where(t => t.IsInterface))
            {
                var implement = allRepositoryImplements.FirstOrDefault(t => t.IsClass && repoType.Name.Substring(1) == t.Name);
                if (implement != null) services.AddScoped(repoType, implement);
            }
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 3;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
