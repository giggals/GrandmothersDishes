using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using GrandmothersDishes.Models;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Receipts
{
    public class DetailsReceiptViewModel
    {
        public int Id { get; set; }

        public ICollection<OrderDrinks> Drinks { get; set; }

        public ICollection<OrderDishes> Dishes { get; set; }

        public ICollection<Order> Orders { get; set; }


        public decimal Total { get; set; }

        public string IssuedOn { get; set; }

        public string Cashier { get; set; }
    }
}
