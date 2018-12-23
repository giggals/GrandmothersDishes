using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class OrderDrinks
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Order Order { get; set; }

        public Drink  Drink { get; set; }

        public int Quantity { get; set; }
    }
}
