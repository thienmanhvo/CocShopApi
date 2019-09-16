using Microsoft.Extensions.Configuration;

namespace CocShop.Data.Appsettings
{
    public class AppSettings
    {
        public static AppSettings Instance { get; set; }
        public static IConfiguration Configs { get; set; }
    }
}
