using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService;
using GrandmothersDishes.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Areas.Administration.Controllers
{
    public class DrinksController : AdministrationBaseController
    {
        public DrinksController(IDrinkService drinkService)
        {
            this.drinkService = drinkService;
        }

        private readonly IDrinkService drinkService;

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(CreateDrinkViewModel drinkModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(drinkModel);
            }

            this.drinkService.CreateDrink(drinkModel);

            return this.Redirect("/Drinks/All");
        }
    }
}