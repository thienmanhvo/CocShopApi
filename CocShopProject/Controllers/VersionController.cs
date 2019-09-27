using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocShop.WebAPi.Controllers
{
    public class VersionController : ControllerBase
    {

        public VersionController()
        {
        }

        [HttpGet("/version")]
        public string GetVersion()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }

        [HttpGet("/TestCICD")]
        public string GetVersion2()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }
    }
}
