using CocShop.Core.Data.Entity;
using CocShop.Data.Appsettings;
using CocShop.WebAPi.Extentions;
using CocShop.WebAPi.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CocShop.WebAPi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.Configs = Configuration;
            AppSettings.Instance = configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddDI();

            #region Setup1
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion
            services.AddIdentity();

            #region Swagger
            services.AddSwaggerDocumentation();
            #endregion

            //#region Cors
            //services.AddCors(options =>
            //options.AddPolicy("AllowAll", builder => builder
            //                        .WithOrigins("http://localhost:4200", "http://localhost:4201")
            //                        .AllowAnyHeader()
            //                        .AllowAnyMethod()
            //                        .AllowCredentials()));
            //#endregion

            services.AddSignalR();
            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //IdentityModelEventSource.ShowPII = true;
            }
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseSwaggerDocumentation();

            app.ConfigureExceptionHandler();

            //#region Identity
            //var task = RolesExtenstions.InitAsync(roleManager);
            //task.Wait();
            //#endregion

            //#region MapsterMapper
            //var map = new MapsterConfig();
            //map.Run();
            //#endregion

            //#region Hangfire
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();
            //#endregion

            app.UseCorsSettings();

            loggerFactory.AddFile(Configuration.GetValue<string>("Logging:LogFilePath"));

            app.UseHttpsRedirection();

            app.UseSignalR(routes =>
            {
                routes.MapHub<CenterHub>("/centerHub");
            });

            app.UseMvc();
        }
    }
}
