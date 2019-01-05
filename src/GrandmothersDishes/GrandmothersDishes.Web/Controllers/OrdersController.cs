using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrandmothersDishes.Services.Constants;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrandmothersDishes.Web.Controllers
{
    public class OrdersController : Controller
    {
        public OrdersController(IOrderService service)
        {
            this.service = service;
        }

        private readonly IOrderService service;

        [Authorize(Roles = "User , Administrator")]
        public IActionResult Order(OrderViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                var controller = this.service.Redirect(orderModel.Id);
                TempData["Message"] = OrderConstants.ErrorMessage;
                return this.RedirectToAction($"Details" , controller, orderModel);
            }
            
            var order = this.service.MakeOrder(orderModel.Id, this.User.Identity.Name, orderModel.Quantity);
            
            return this.RedirectToAction("MyOrders");
        }

        [Authorize(Roles = "User , Administrator")]
        public IActionResult MyOrders()
        {
            var model = this.service.GetAllActiveOrders(this.User.Identity.Name);

            return this.View(model);
        }
    }
}
