using System;
using System.Collections.Generic;
using System.Linq;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.OrdersService
{
    public class OrderService : IOrderService
    {
        public OrderService(IRepository<Order> ordersRepository,
            IRepository<Dish> dishRepository,
            IRepository<Drink> drinkRepository,
            IRepository<GrandMothersUser> usersRepository,
            IRepository<OrderDrinks> orderDrinksRepository,
            IRepository<OrderDishes> orderDishesRepository
        )
        {
            this.ordersRepository = ordersRepository;
            this.dishRepository = dishRepository;
            this.drinkRepository = drinkRepository;
            this.usersRepository = usersRepository;
            this.orderDrinksRepository = orderDrinksRepository;
            this.orderDishesRepository = orderDishesRepository;
        }

        private readonly IRepository<Order> ordersRepository;
        private readonly IRepository<Dish> dishRepository;
        private readonly IRepository<Drink> drinkRepository;
        private readonly IRepository<GrandMothersUser> usersRepository;
        private readonly IRepository<OrderDrinks> orderDrinksRepository;
        private readonly IRepository<OrderDishes> orderDishesRepository;

        public Order MakeOrder(string id , string username , int quantity)
        {
            var order = new Order()
            {
                OrderedOn = DateTime.UtcNow,
                Status = Status.Active,
                
            };

            var dish = this.dishRepository.All().FirstOrDefault(x => x.Id == id);
            if (dish != null)
            {
                var orderDish = new OrderDishes()
                {
                    Dish = dish,
                    Order = order,
                    Quantity = quantity,
                };

                order.Dishes.Add(orderDish);
            }

            var drink = this.drinkRepository.All().FirstOrDefault(x => x.Id == id);
            if (drink != null)
            {
                var orderDrink = new OrderDrinks()
                {
                    Drink = drink,
                    Order = order,
                    Quantity = quantity,
                };

                order.Drinks.Add(orderDrink);
            }

            var user = this.GetCurrentUser(username);
            order.User = user;

            this.ordersRepository.AddAsync(order);
            this.ordersRepository.SaveChanges();
                
            return order;

        }

        public GrandMothersUser GetCurrentUser(string username)
        {
            var currentUser = this.usersRepository.All().FirstOrDefault(x => x.UserName == username);

            return currentUser;
        }

        public ICollection<MyOrdersViewModel> GetMyActiveDishOrders(string username)
        {
            var user = this.GetCurrentUser(username);

            var dishesOrders = this.orderDishesRepository.All()
                .Where(x => x.Order.Status == Status.Active)
                .Select(x =>  new MyOrdersViewModel()
                {
                    Name = x.Dish.Name,
                    Price = x.Dish.Price,
                    Quantity = x.Quantity,
                })
                .ToList();
            
            return dishesOrders;

        }

        public ICollection<MyOrdersViewModel> GetMyActiveDrinksOrders(string username)
        {
            var user = this.GetCurrentUser(username);

            var drinksOrders = this.orderDrinksRepository.All()
                .Where(x => x.Order.Status == Status.Active)
                .Select(x => new MyOrdersViewModel()
                {
                    Name = x.Drink.Name,
                    Price = x.Drink.Price,
                    Quantity = x.Quantity,
                })
                .ToList();
           

            return drinksOrders;

        }

        public AllMyOrdersViewModel GetAllActiveOrders(string username)
        {
            var dishesOrders = this.GetMyActiveDishOrders(username);

            var drinksOrders = this.GetMyActiveDrinksOrders(username);

            decimal total = 0;

            foreach (var dish in dishesOrders)
            {
                total += (dish.Price * dish.Quantity);
            }

            foreach (var drink in drinksOrders)
            {
                total += (drink.Price * drink.Quantity);
            }

            return new AllMyOrdersViewModel()
            {
                DrinksOrders = drinksOrders,
                DishesOrders = dishesOrders,
                Total = total,
            };

        }

        public ICollection<Order> GetAllOrders(string username)
        {
            var orders = this.ordersRepository.All()
                .Where(x => x.Status == Status.Active)
                .Where(x => x.User.UserName == username)
                .ToList();

            return orders;
        }

        public string Redirect(string id)
        {
            var result = string.Empty;

            var drink = this.drinkRepository.All().FirstOrDefault(x => x.Id == id);
            if (drink != null)
            {
                result = $"Drinks";
            }
            else
            {
                result = $"Foods";
            }

            return result;

        }

        

    }
}
