using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DrinksService
{
    public interface IDrinkService
    {
        AllDrinksViewModel GetAllDrinks();

        Task CreateDrink(CreateDrinkViewModel drinkModel);

        DrinkDetailsViewModel GetDrinkModel(string id);

        void EditDrink(DrinkEditDeleteViewModel editModel);

        DrinkEditDeleteViewModel EditDeleteDrinkGetModel(string id);

        void DeleteDrink(string id);
    }
}
