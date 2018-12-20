using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService;
using GrandmothersDishes.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Areas.Administration.Controllers
{
    public class VehiclesController : AdministrationBaseController
    {
        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        private readonly IVehicleService vehicleService;

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(CreateVehicleViewModel vehicleModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vehicleModel);
            }

            this.vehicleService.CreateVehicle(vehicleModel);

            return this.Redirect("/");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All(VehicleViewModel vehicleModel)
        {
            var model = this.vehicleService.GetAllVehicles(vehicleModel);


            return this.View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            var model = this.vehicleService.GetVihecleViewModel(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id , string name)
        {
            this.vehicleService.DeleteVehicle(id);

            return this.RedirectToAction("All");
        }


    }
}