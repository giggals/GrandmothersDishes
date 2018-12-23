using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Order
    {
        public Order()
        {
            this.Dishes = new HashSet<OrderDishes>();

            this.Drinks = new HashSet<OrderDrinks>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public GrandMothersUser User { get; set; }

        public DateTime OrderedOn { get; set; }

        public Status Status { get; set; }

        public ICollection<OrderDishes> Dishes { get; set; }

        public ICollection<OrderDrinks> Drinks { get; set; }


    }
}
