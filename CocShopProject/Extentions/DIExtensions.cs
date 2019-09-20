using AutoMapper;
using CocShop.Data.Infrastructure;
using CocShop.Data.Repositories;
using CocShop.Service.AutoMapper;
using CocShop.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocShopProject.Extentions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            AddServiceDI(services);
            AddRepoistoryDI(services);
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //EventLog


            //SignalR
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region automapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion
            return services;
        }

        public static void AddServiceDI(IServiceCollection services)
        {
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IProductService, ProductService>();
        }

        public static void AddRepoistoryDI(IServiceCollection services)
        {
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

    }
}
