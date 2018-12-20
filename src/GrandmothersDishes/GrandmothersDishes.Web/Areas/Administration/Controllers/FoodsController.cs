using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Areas.Admin.Controllers
{
    public class FoodsController : AdministrationBaseController
    {
        public FoodsController(IFoodService dishService)
        {
            this.dishService = dishService;
        }

        private readonly IFoodService dishService;

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(CreateDishViewModel dishModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(dishModel);
            }

            var dish = this.dishService.CreateDish(dishModel);

            return this.Redirect("/Home/Index");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id)
        {
            var viewModel = dishService.EditDeleteDishGetModel(id);


            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(UpdateDeleteViewModel editModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editModel);
            }

            this.dishService.EditDish(editModel);

            return this.Redirect($"/Foods/Details?id={editModel.Id}");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            var viewModel = dishService.EditDeleteDishGetModel(id);

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Delete(string id, string name)
        {
            this.dishService.DeleteDish(id);

            return this.Redirect("/");
        }

    }



}
