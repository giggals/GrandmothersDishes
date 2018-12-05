using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class OrderDishes
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Order Order { get; set; }

        public Dish Dish{ get; set; }

    }
}
