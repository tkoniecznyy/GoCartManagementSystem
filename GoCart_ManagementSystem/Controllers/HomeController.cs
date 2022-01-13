using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoCart_ManagementSystem.Models;
using GoCart_ManagementSystem.Data;

namespace GoCart_ManagementSystem.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {
     
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
