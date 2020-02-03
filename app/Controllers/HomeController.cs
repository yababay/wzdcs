using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions; 
using Microsoft.Extensions.Logging;
using app.Models;
using yababay.wzdcs;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string BaseUrl = "http://localhost:9699";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("wzdcs/test/put")]
        public string TestPut()
        {
            string url = BaseUrl + Request.Path.ToString() + "/sobaka.txt";
            WZDResponse resp = WZDClient.PutFile(url, "У попа была собака.");
            return resp.Description;
        }

        /*
        [HttpGet]
        [Route("wzdcs/test/post")]
        public string TestPost()
        {
            WZDResponse resp = WZDClient.PutFile("http://localhost:9699/wzd/test/test1.txt", "У попа была собака");
            Console.WriteLine("resp", resp.Response);
            Console.WriteLine("descr", resp.Description);
            return "OK";
        }
        */

        [HttpGet]
        [Route("wzdcs/test/get")]
        public string TestGetPut()
        {
            string url = BaseUrl + "/wzdcs/test/put/sobaka.txt";
            WZDResponse resp = WZDClient.GetFile(url);
            return resp.Response;
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
