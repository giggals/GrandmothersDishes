using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService
{
    public interface IOrderService
    {
        Order MakeOrder(string id, string username, int quantity);

        GrandMothersUser GetCurrentUser(string id);

        ICollection<MyOrdersViewModel> GetMyActiveDishOrders(string username);

        ICollection<MyOrdersViewModel> GetMyActiveDrinksOrders(string username);

        AllMyOrdersViewModel GetAllActiveOrders(string username);

        ICollection<Order> GetAllOrders(string username);

         string Redirect(string id);
    }
}
