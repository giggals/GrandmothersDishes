using GrandmothersDishes.Services.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks
{
    public class CreateDrinkViewModel
    {
        [Required]
        [StringLength(DrinkConstants.MaxDrinkNameLenght, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = DrinkConstants.MinDrinkNameLenght)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), GlobalConstants.MinCalories, GlobalConstants.MaxCalories)]
        public decimal Calories { get; set; }

        [Required]
        [Range(typeof(decimal), GlobalConstants.MinPriceValueAsString, GlobalConstants.decimalMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DrinkConstants.MaxDescriptionSymbols, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = DrinkConstants.MinDescriptionSymbols)]
        public string Description { get; set; }

        [Required]
        public string DrinkType { get; set; }
    }
}
