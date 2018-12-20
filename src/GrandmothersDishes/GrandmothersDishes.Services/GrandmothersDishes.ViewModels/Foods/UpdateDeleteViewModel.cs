using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods
{
    public class UpdateDeleteViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(FoodConstants.MaxDishNameLenght, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = FoodConstants.MinDishNameLenght)]
        public string Name { get; set; }

        [Required]
        [StringLength(FoodConstants.MaxDescriptionSymbols, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = FoodConstants.MinDescriptionSymbols)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), FoodConstants.MinCalories, FoodConstants.MaxCalories)]
        public decimal Calories { get; set; }

        [Required]
        public string DishType { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), FoodConstants.minPriceValueAsString, GlobalConstants.decimalMaxValue)]
        public decimal Price { get; set; }

    }
}
