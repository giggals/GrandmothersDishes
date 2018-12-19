using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class FoodsController : Controller
    {
        public FoodsController(IFoodService foodService)
        {
            this.foodService = foodService;
        }

        private readonly IFoodService foodService;

        [Authorize(Roles = "User , Administrator")]
        public IActionResult Details(string id)
        {
            var model = this.foodService.GetDishDetails(id);

            if (model == null)
            {
                return this.BadRequest("Invalid dish Id");
            }
            
            return View(model);
        }
    }
}