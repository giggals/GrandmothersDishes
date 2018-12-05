using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Dish
    {
        public Dish()
        {
            this.Orders = new HashSet<OrderDishes>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public decimal Calories { get; set; }

        public DishType DishType { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public ICollection<OrderDishes> Orders { get; set; }
        
    }
}
