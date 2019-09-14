using Microsoft.Extensions.Configuration;

namespace CocShop.Core.Appsettings
{
    public class AppSettings
    {
        public static AppSettings Instance { get; set; }
        public static IConfiguration Configs { get; set; }
    }
}
