using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core;
using General.Entities;
using General.Framework;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace General.Mvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 数据库连接  ORM
            services.AddDbContext<GeneralDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region 开启认证
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAdminAuthInfo.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAdminAuthInfo.AuthenticationScheme;
                o.DefaultSignInScheme = CookieAdminAuthInfo.AuthenticationScheme;
                o.DefaultSignOutScheme = CookieAdminAuthInfo.AuthenticationScheme;
            }).AddCookie(CookieAdminAuthInfo.AuthenticationScheme, o =>
              {
                  o.LoginPath = "/admin/login";
              });
            #endregion

            #region 依赖注入 

            //services.AddScoped<ICategoryService, CatergoyService>();
            services.AddAssembly("General.Services");

            //泛型注入到DI中
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IWorkContext, WorkContext>();

            services.AddScoped<IAdminAuthService, AdminAuthService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMemoryCache, MemoryCache>();

            #endregion


            //services.BuildServiceProvider().GetService<ICategoryService>();

            EngineContext.Initial(new GeneralEngine(services.BuildServiceProvider()));
            //services.AddScoped<IServiceProvider, ServiceProvider>();
            //services.AddScoped<IEngine, GeneralEngine>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            #region 开启认证
            app.UseAuthentication();
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                );
            });
        }
    }
}
