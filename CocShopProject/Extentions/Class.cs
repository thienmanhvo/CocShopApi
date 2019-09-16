using CocShop.Core.Configs;
using CocShop.Data.Appsettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace CocShopProject.Extentions
{
    public static class CorsExtentions
    {
        public static IApplicationBuilder UseCorsSettings(this IApplicationBuilder app)
        {
            var corsSettings = AppSettings.Configs.GetSection("CorsSettings").Get<CorsSettings>();
            if (!string.IsNullOrEmpty(corsSettings.AllowOrigin))
            {
                app.UseCors(m =>
                {
                    if (corsSettings.AllowOrigin == "*")
                    {
                        m.AllowAnyOrigin();
                    }
                    else
                    {
                        m.WithHeaders(corsSettings.AllowOrigin.Split(",", StringSplitOptions.RemoveEmptyEntries));
                    }

                    if (corsSettings.AllowMethod == "*")
                    {
                        m.AllowAnyMethod();
                    }
                    else
                    {
                        m.WithHeaders(corsSettings.AllowMethod.Split(",", StringSplitOptions.RemoveEmptyEntries));
                    }

                    if (corsSettings.AllowHeader == "*")
                    {
                        m.AllowAnyHeader();
                    }
                    else
                    {
                        m.WithHeaders(corsSettings.AllowHeader.Split(",", StringSplitOptions.RemoveEmptyEntries));
                    }
                });
            }
            return app;
        }
    }
}
