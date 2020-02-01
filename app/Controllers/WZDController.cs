using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using yababay.wzdcs;

namespace app.Controllers
{
    public class WZDController : Controller
    {
        private readonly ILogger<WZDController> _logger;

        public WZDController(ILogger<WZDController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("wzd/test")]
        public string Test()
        {
            WZDResponse resp = WZDClient.SentStringRequest("http://localhost:9699/wzd/test/test.txt", "У попа была собака", null, "PUT");
            Console.WriteLine(resp.Response);
            Console.WriteLine(resp.Description);
            return "OK";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
