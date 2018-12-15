using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Drink
    {
        public Drink()
        {
            this.Orders = new HashSet<OrderDrinks>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public decimal Calories { get; set; }

        public DrinkType DrinkType { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public ICollection<OrderDrinks> Orders { get; set; }

    }
}
