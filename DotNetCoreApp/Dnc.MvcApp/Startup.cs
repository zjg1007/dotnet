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
using UEditorNetCore;

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
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            //第一个参数为配置文件路径，默认为项目目录下config.json
            //第二个参数为是否缓存配置文件，默认false
            services.AddUEditorService();
            // 添加 MVC 框架
            services.AddMvc(options=> 
            {
                //options.Filters.Add(typeof(AppAuthorityFilter)); // 类型方式注入
            });
            // 添加支付宝客户端依赖注入
            //services.AddAlipay(opt =>
            //{
            //    //此处为蚂蚁金服开放平台上创建的APPID，而非老版本的商户号
            //    opt.AppId = "2017060307410939";

            //    // 这里的公私钥 默认均为支付宝官方推荐使用的RSAWithSHA256.
            //    // 商户私钥
            //    opt.RsaPrivateKey = "/ykZcAHxJqW1G+gEG3m4AQ==";
            //    // 支付宝公钥
            //    opt.RsaPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqx3sTkM63I3hcDKWSWwegy02TmJw5Vcq1NoglDuN9J9A53UycpyAnkCedslG1CID1xwVwEkeoivEvuytu3CEkdMmFM84q65QAAzBa56cd1BykI35Xkv19g/xOsim5QxRfQYG/GlSpW2T++cDA6QI3f5KOnaEL8tUYm6/EkhW2s2WEAg3Xnk28Q2hBM/PxGxfPpThVP9x5b7X26mQ5flfLgi7cwoJZdlfM6kZzKiupxv3yaePaM4onhz6ecW4Z+Mamx9T0ELhepL6OQkw45Nbvzma/kUkFOIOPNP3GdJxFc8LnGy6cmxBb5PMjIbBADj9rMjixa7agifP213iftOOZQIDAQAB";
            //});
            // 添加跨域访问授权处理
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("Dnc", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            // 注册配置实例
            //services.Configure<AlipayOptions>(Configuration.GetSection("Alipay"));
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
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

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
