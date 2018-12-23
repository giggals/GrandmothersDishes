using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Drinks
{
    public class DrinkDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Calories { get; set; }

        public string DrinkType { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
