using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Problem1ReplaceWord.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

namespace Problem1ReplaceWord.Controllers
{
    public class HomeController : Controller
    {
      

        private readonly ILogger<HomeController> _logger;

        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult GetInfo(string word, string replaceword, IFormFile location) 
        {
            var word1= word;
            var replace1= replaceword;
            var location1= location;
            var p = location.FileName;

            // save file this project
            using (var filestrm = new FileStream(Path.Combine(p), FileMode.Create, FileAccess.ReadWrite))
            {
                location.CopyTo(filestrm);

            }

            // replace word
            string path = Path.GetFullPath(location.FileName);
            using ( var t = new StreamReader(p)) 
            {
                string str1 = System.IO.File.ReadAllText(path);
                   string s = str1;
                       s = s.Replace(word1, replace1);// want replace "Reza" with Behnaz"
                   System.IO.File.WriteAllText(Path.Combine("Text.txt"), s);

            }
                
                return RedirectToAction("Privacy");
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
