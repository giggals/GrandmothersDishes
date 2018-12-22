using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class DrinksController : Controller
    {
        public DrinksController(IDrinkService drinkService)
        {
            this.drinkService = drinkService;
        }

        private readonly IDrinkService drinkService;

        public IActionResult All(DrinkViewModel drinkModel)
        {
            var allDrinks = this.drinkService.GetAllDrinks(drinkModel);

            return View(allDrinks);
        }

        public IActionResult Details(string id)
        {
            var model = this.drinkService.GetDrinkModel(id);

            return this.View(model);
        }

    }
}