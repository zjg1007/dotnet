using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dnc.MvcApp.ViewInjections;
using Dnc.MvcApp.Filters;
using Dnc.MvcApp.Utilities;
using Dnc.DataAccessRepository.Context;
using Dnc.DataAccessRepository.Repositories;
using Dnc.DataAccessRepository.Seeds;

namespace Dnc.MvcApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // 运行时被掉用的方法，用于向程序容器注入服务
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加 MVC 框架
            services.AddMvc(options=> 
            {
                //options.Filters.Add(typeof(AppAuthorityFilter)); // 类型方式注入
            });

            // 注入自定义的用户认证过滤器
            services.AddScoped<AppAuthorityFilter>();

            // 注入 AppDbContext 对应的数据库连接串
            services.AddDbContext<EntityDbContext>();
            //services.AddDbContext<AppDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("AppConnection")));

            // 注入Session组件
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.Name = ".MyCoreApp";
            });

            // 添加文章注入的例子
            services.AddTransient<ArticleInjection>();

            #region 用于配置业务实体在控制器中处理服务注入的处理
            //services.AddTransient<DbContext, EntityDbContext>();
            services.AddTransient<IEntityRepository,EntityRepository>();

            #endregion

        }

        // 运行时被调用的方法，用于配置 HTTP 请求管道
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EntityDbContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // 启用 Session，添加 Session 的引用
            app.UseSession();

            app.UseStaticFiles();

            // MVC 路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            // 处理种子数据
            DbInitializer.Initialze(context);

            // 初始化一些静态类
            DataAccessUtility.InitialDBContext(context);
        }
    }
}
