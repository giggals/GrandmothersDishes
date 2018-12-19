using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods;
using GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.FoodService
{
    public interface IFoodService
    {
        Dish CreateDish(CreateDishViewModel dishViewModel);

        DetailsDishViewModel GetDishDetails(string id);

    }
}
