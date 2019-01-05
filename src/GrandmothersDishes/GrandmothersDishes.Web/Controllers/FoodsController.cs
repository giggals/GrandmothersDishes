
using GrandmothersDishes.Services.Constants;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class FoodsController : Controller
    {
        public FoodsController(IFoodService foodService)
        {
            this.foodService = foodService;
        }

        private readonly IFoodService foodService;

        [Authorize(Roles = "User , Administrator")]
        public IActionResult Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var model = this.foodService.GetDishDetails(id);

            if (model == null)
            {
                this.ViewData[GlobalConstants.ModelDishError] = GlobalConstants.NullIdDish;
                return this.View();
            }
            
            return View(model);
        }
    }
}