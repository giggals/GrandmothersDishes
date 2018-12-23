using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders
{
    public class AllMyOrdersViewModel
    {
        public ICollection<MyOrdersViewModel> DishesOrders { get; set; }

        public ICollection<MyOrdersViewModel> DrinksOrders { get; set; }
        
        public decimal Total { get; set; }
    }
}
