using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.Constants;
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
        public async Task<IActionResult> Create(CreateDrinkViewModel drinkModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(drinkModel);
            }

            await this.drinkService.CreateDrink(drinkModel);

            return this.Redirect("/Drinks/All");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id)
        {
            var viewModel = drinkService.EditDeleteDrinkGetModel(id);

            if (viewModel == null)
            {
                this.ViewData[GlobalConstants.ModelDrinkError] = GlobalConstants.NullDrink;
                return this.View();
            }
            
            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(DrinkEditDeleteViewModel editModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editModel);
            }

            this.drinkService.EditDrink(editModel);

            return this.Redirect($"/Drinks/Details?id={editModel.Id}");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            var viewModel = drinkService.EditDeleteDrinkGetModel(id);

            if (viewModel == null)
            {
                this.ViewData[GlobalConstants.ModelDrinkError] = GlobalConstants.NullDrink;
                return this.View();
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Delete(string id, string name)
        {
            this.drinkService.DeleteDrink(id);

            return this.Redirect("/Drinks/All");
        }

    }
}