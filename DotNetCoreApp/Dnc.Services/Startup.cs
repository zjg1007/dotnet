using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dnc.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Dnc.DataAccessRepository.Context;
using Dnc.DataAccessRepository.Repositories;

namespace Dnc.Services
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

            BusinessDataAccess.Set();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
         
            // 添加跨域访问授权处理
            services.AddCors(option => option.AddPolicy("DncDemo", p => p.WithOrigins("http://localhost:8166", 
                "http://localhost:8166").AllowAnyHeader().AllowAnyMethod()));

            // 注入 DbContext 对应的数据库连接实例
            services.AddDbContext<EntityDbContext>();

            // 注入 数据服务接口实例;
            services.AddTransient<IEntityRepository, EntityRepository>();

            // 注入Session组件
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.CookieName = ".MyCoreApp";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // 启用 Session，添加 Session 的引用
            app.UseSession();

            app.UseMvc();
            app.UseCors("DncDemo");
        }
    }
}
