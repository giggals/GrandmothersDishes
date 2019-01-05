
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Vehicles;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.VehicleService;
using GrandmothersDishes.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GrandmothersDishes.Services.Constants;

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
        public async Task<IActionResult> Create(CreateVehicleViewModel vehicleModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(vehicleModel);
            }

           await this.vehicleService.CreateVehicle(vehicleModel);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            var model = this.vehicleService.GetAllVehiclesWithViewModel();
            
            return this.View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            var model = this.vehicleService.GetDeleteVihecleViewModel(id);

            if (model == null)
            {
                this.ViewData[GlobalConstants.ModelVehicleError] = GlobalConstants.NullIdVehicle;
                return this.View();
            }

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