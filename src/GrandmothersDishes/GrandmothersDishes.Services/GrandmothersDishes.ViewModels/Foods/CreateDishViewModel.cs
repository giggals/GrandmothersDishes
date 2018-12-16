using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandmothersDishes.Web.Areas.Administration.Models.FoodsViewModels
{
    public class CreateDishViewModel
    {
        private const string NameLenghtErrorMessage = "Please supply at least {2} characters.";

        private const string decimalMaxValue = "79228162514264337593543950335";

        private const string minPriceValue = "0";

        private const string minCalories = "0";

        private const string maxCalories = "999999";

        [Required]
        [StringLength(15, ErrorMessage = NameLenghtErrorMessage, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), minCalories, maxCalories)]
        public decimal Calories { get; set; }

        [Required]
        [Range(typeof(decimal), minPriceValue, decimalMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DishType { get; set; }

    }
}
