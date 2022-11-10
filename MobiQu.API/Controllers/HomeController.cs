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

        [HttpGet("test-url2")]
        public IActionResult ConnectUrlBase()
        {
            return Ok(new { message = "bases" });
        }

        [HttpGet("mobiqu")]
        public IActionResult Connection()
        {
            return Ok(new { message = "mobiqu" });
        }
    }
}
