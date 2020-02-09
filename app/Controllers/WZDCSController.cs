using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions; 
using Microsoft.Extensions.Logging;
using app.Models;
using yababay.wzdcs;

namespace app.WZDCSControllers
{
    public class WZDCSController : Controller
    {
        private readonly ILogger<WZDCSController> _logger;
        private readonly string BaseUrl = "http://localhost:9699/wzdcs/";

        public WZDCSController(ILogger<WZDCSController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string TestPut()
        {
            Console.WriteLine("PUT");
            string url = BaseUrl + "sobaka.txt";
            WZDResponse resp = WZDClient.PutFile(url, "У попа была собака.");
            return resp.Description;
        }

        [HttpPost]
        public string TestPost([FromForm] string content)
        {
            string url = BaseUrl + "sobaka.json";
            WZDResponse resp = WZDClient.PostFile(url, content);
            return resp.Description;
        }

        [HttpPost]
        public string TestBinary(List<IFormFile> binary)
        {
            long size = binary.Sum(f => f.Length);
            foreach (var formFile in binary)
            {
                if (formFile.Length > 0)
                {
                    string url = BaseUrl + formFile.FileName; 
                    Console.WriteLine(url);
                    Console.WriteLine(formFile.Length);
                    Stream requestStream = WZDClient.GetStream(url, formFile.Length);
                    formFile.CopyTo(requestStream);
                    requestStream.Flush();
                    requestStream.Close();
                }
            }
            return "OK";
        }

        [HttpGet]
        public string TestGet(int id)
        {
            string ext = "txt";
            if(id == 1) ext = "json";
            Console.WriteLine("GET");
            string url = BaseUrl + "sobaka." + ext;
            WZDResponse resp = WZDClient.GetFile(url);
            return resp.Response;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
