using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class DiscountCardsController : Controller
    {
        public DiscountCardsController(IDiscountCardService service)
        {
            this.service = service;
        }

        private readonly IDiscountCardService service;

        [Authorize(Roles = "User , Administrator")]
        public IActionResult All()
        {
            var model = this.service.GetAllCards();

            return View(model);
        }
    }
}