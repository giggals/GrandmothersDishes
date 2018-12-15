using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.HomeService;
using Microsoft.AspNetCore.Mvc;
using GrandmothersDishes.Web.Models;

namespace GrandmothersDishes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }
        
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var dishesModel = this.homeService.AllProducts();

                return this.View("LoggedInIndex", dishesModel);
            }

            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
