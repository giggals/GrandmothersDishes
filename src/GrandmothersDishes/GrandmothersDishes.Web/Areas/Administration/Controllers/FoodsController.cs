﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}