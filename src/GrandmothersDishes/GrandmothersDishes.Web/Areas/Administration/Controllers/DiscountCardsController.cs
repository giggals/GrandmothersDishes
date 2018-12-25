using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService;
using GrandmothersDishes.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Areas.Administration.Controllers
{
    public class DiscountCardsController : AdministrationBaseController
    {
        public DiscountCardsController(IDiscountCardService service)
        {
            this.service = service;
        }

        private readonly IDiscountCardService service;

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(CreateCardViewModel cardModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(cardModel);
            }

            this.service.CreateCard(cardModel);

            return Redirect("/");
        }
    }
}