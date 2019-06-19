using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Tenancy_Contract.DataAccessLayer;
using TenancyContract.Entities;
using Microsoft.AspNetCore.Identity;
using TenancyContract.Identity;
using Web.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rotativa.AspNetCore;
using Web.Repository;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace TenancyContract
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;

            });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("bn")
                    };
                    options.DefaultRequestCulture = new RequestCulture("en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
            services.AddDbContext<TenancyContractDbContext>(options =>
               options.UseSqlServer(
               Configuration.GetConnectionString("TenancyContractDB")));
            services.AddIdentityCore<Tenant>(options =>
            {

            });
            services.AddIdentityCore<HouseOwner>(options =>
            {
            });
            services.AddScoped<IUserStore<Tenant>, TenantUserStore>();
            services.AddScoped<IUserStore<HouseOwner>, HouseOwnerUserStore>();
            services.AddAuthentication("cookies")
                .AddCookie("cookies", options => options.LoginPath = "/Account/Login");
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyTenant", policy =>
                policy.RequireRole("Tenant"));
                options.AddPolicy("OnlyHO", policy =>
                policy.RequireRole("Houseowner"));
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages"))
            );
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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


            app.UseStaticFiles();
            //TODO: Implement Error Status Code! 
            app.UseStatusCodePages();
            app.UseSession();
            //app.UseStatusCodePagesWithReExecute("/Status{0}");
            app.UseAuthentication();
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseMvc(routes =>
            {

                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            RotativaConfiguration.Setup(env);
        }
    }
}
