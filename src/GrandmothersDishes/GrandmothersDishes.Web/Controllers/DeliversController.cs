using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDIshes.DeliverService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class DeliversController : Controller
    {
        public DeliversController(IDeliverService service)
        {
            this.service = service;
        }

        private readonly IDeliverService service;

        [Authorize(Roles = "User , Administrator")]
        public IActionResult Deliver()
        {
            return View();
        }

        [Authorize(Roles = "User , Administrator")]
        [HttpPost]
        public IActionResult Deliver(DeliverViewModel deliverModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(deliverModel);
            }

            this.service.Deliver(deliverModel, this.User.Identity.Name);

            return View();
        }
    }
}