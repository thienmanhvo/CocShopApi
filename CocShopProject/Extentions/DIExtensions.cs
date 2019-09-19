using CocShop.Data.Infrastructure;
using CocShop.Data.Repositories;
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
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //EventLog


            //SignalR
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IHubUserConnectionRepository, HubUserConnectionRepository>();
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationRepository, NotificationRepository>();

            return services;
        }

    }
}
