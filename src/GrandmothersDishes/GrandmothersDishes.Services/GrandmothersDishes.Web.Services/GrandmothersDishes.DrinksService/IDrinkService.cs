using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService
{
    public interface IDrinkService
    {
        AllDrinksViewModel GetAllDrinks();

        void CreateDrink(CreateDrinkViewModel drinkModel);

        DrinkDetailsViewModel GetDrinkModel(string id);

        void EditDrink(DrinkEditDeleteViewModel editModel);

        DrinkEditDeleteViewModel EditDeleteDrinkGetModel(string id);

        void DeleteDrink(string id);
    }
}
