using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards
{
    public class CreateCardViewModel
    {
        [Required]
        [Range(typeof(decimal), DiscountConstants.MinPercantage, DiscountConstants.MaxPercantage)]
        public decimal DiscountPercentage { get; set; }

        [Required]
        public string DiscountType { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DiscountConstants.MaxDescriptionSymbols, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = DiscountConstants.MinDescriptionSymbols)]
        public string Description { get; set; }


    }
}
