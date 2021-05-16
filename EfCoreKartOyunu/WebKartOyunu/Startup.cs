using BusinessLayer;
using CORE_HBKSOFTWARE.Classes;
using CORE_HBKSOFTWARE.Interfaces;
using CORE_HBKSOFTWARE.Models;
using DataAccesLayer;
using EntityLayer;
using EntityLayer.GameCart;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKartOyunu
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
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddMvc();
            services.AddDbContext<KartOyunuDBContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Account/Login";
                    x.AccessDeniedPath = "/Account/AccessDenied";
                });
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            var emailConfig = Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfigModel>();

            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<IRecaptchaValidator, RecaptchaValidator>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddSingleton<IPasswordHashing, PasswordHashing>();
            services.AddSingleton<IFileUploader, FileUploader>();
            services.AddSingleton<FileRepo>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KartOyunuDBContext db)
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
            db.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
