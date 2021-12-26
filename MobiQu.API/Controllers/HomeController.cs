using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        [HttpGet("test-url")]
        public IActionResult ConnectUrl()
        {
            return Ok(new { message = "anasayfa" });
        }
    }
}
