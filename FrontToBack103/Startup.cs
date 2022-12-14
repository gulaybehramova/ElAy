using FrontToBack103.DAL;
using FrontToBack103.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack103
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
            });
            services.AddSession(opt =>
            {
                opt.IdleTimeout=TimeSpan.FromMinutes(20);
            });

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength=6;
                opt.Password.RequireDigit=true;
                opt.Password.RequireLowercase=true;
                opt.Password.RequireNonAlphanumeric=true;
                opt.Password.RequireUppercase=true;

                opt.User.RequireUniqueEmail=true;
                opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(60);
                opt.Lockout.MaxFailedAccessAttempts=3;
                opt.Lockout.AllowedForNewUsers=true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "areas",
                    "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
                  );
                endpoints.MapControllerRoute(
                    "default", 
                    "{controller=home}/{action=index}/{id?}"
                    );
            });
        }
    }
}
