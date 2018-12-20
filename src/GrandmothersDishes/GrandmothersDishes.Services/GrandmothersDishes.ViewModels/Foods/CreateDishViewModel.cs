using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels
{
    public class CreateDishViewModel
    {
        [Required]
        [StringLength(FoodConstants.MaxDishNameLenght, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = FoodConstants.MinDishNameLenght)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), FoodConstants.MinCalories, FoodConstants.MaxCalories)]
        public decimal Calories { get; set; }

        [Required]
        [Range(typeof(decimal), FoodConstants.minPriceValueAsString , GlobalConstants.decimalMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(FoodConstants.MaxDescriptionSymbols, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = FoodConstants.MinDescriptionSymbols)]
        public string Description { get; set; }

        [Required]
        public string DishType { get; set; }

    }
}
