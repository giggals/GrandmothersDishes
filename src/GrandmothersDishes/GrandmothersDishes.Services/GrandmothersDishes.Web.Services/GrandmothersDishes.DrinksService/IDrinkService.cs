using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService
{
    public interface IDrinkService
    {
        AllDrinksViewModel GetAllDrinks(DrinkViewModel drinkModel);

        void CreateDrink(CreateDrinkViewModel drinkModel);
    }
}
