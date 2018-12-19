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
        public FoodsController(IFoodService service)
        {
            this.service = service;
        }

        private readonly IFoodService service;

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

            var dish = this.service.CreateDish(dishModel);

            return this.Redirect("/Home/Index");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id)
        {
            var viewModel = service.EditDeleteDishGetModel(id);


            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(UpdateDeleteViewModel editModel)
        {
            this.service.EditDish(editModel);

            return this.Redirect($"/Foods/Details?id={editModel.Id}");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            var viewModel = service.EditDeleteDishGetModel(id);

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Delete(string id, string name)
        {
            this.service.DeleteDish(id);

            return this.Redirect("/");
        }

    }



}
