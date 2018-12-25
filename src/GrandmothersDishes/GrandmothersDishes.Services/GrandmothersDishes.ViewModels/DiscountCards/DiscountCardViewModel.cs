using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.DiscountCards
{
    public class DiscountCardViewModel
    {
        public decimal DiscountPercentage { get; set; }

        public string DiscountType { get; set; }

        public string ImageUrl { get; set; }
        
        public string Description { get; set; }
    }
}
