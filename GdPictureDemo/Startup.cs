using GdPicture14.WEB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace GdPictureDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DocuViewareLicensing.RegisterKEY("0449326832734035850501424");
            string cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
            string appUri = "https://localhost:44313";
            string docuViewareApi = "api/DocuVieware";
            DocuViewareManager.SetupConfiguration(true,
                DocuViewareSessionStateMode.InProc,
                cacheDir, appUri, docuViewareApi);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            services.AddTransient<DocuViewareCustomActionsHandler>();

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#else
            services.AddRazorPages();
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            DocuViewareEventsHandler.CustomAction += (sender, e) =>
            {
                var handler = app.ApplicationServices.GetService<DocuViewareCustomActionsHandler>();
                switch (e.actionName)
                {
                    case "SetStar":
                        handler.SetStar(e);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            };

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
