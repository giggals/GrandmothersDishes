using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using GrandmothersDishes.Data.RepositoryPattern.Contracts;
using GrandmothersDishes.Models;
using GrandmothersDishes.Models.Enums;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;
using GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders;
using GrandmothersDishes.Services.GrandmothersDishes.Web.Services.GrandmothersDishes.DiscountCardService;
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
            IRepository<OrderDishes> orderDishesRepository,
            IRepository<DiscountCard> cardRepository,
            IDiscountCardService cardService
        )
        {
            this.ordersRepository = ordersRepository;
            this.dishRepository = dishRepository;
            this.drinkRepository = drinkRepository;
            this.usersRepository = usersRepository;
            this.orderDrinksRepository = orderDrinksRepository;
            this.orderDishesRepository = orderDishesRepository;
            this.cardRepository = cardRepository;
            this.cardService = cardService;
        }

        private readonly IRepository<Order> ordersRepository;
        private readonly IRepository<Dish> dishRepository;
        private readonly IRepository<Drink> drinkRepository;
        private readonly IRepository<GrandMothersUser> usersRepository;
        private readonly IRepository<OrderDrinks> orderDrinksRepository;
        private readonly IRepository<OrderDishes> orderDishesRepository;
        private readonly IRepository<DiscountCard> cardRepository;
        private readonly IDiscountCardService cardService;

        public Order MakeOrder(string id, string username, int quantity)
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
                .Where(x => x.Order.User == user)
                .Select(x => new MyOrdersViewModel()
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
                .Where(x => x.Order.User == user)
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

            var user = this.GetCurrentUser(username);
            decimal discountDifference = 0;
            
           
            if (total >= 250 && user.DiscountCards.Count == 0)
            {
                var vipCard = this.cardRepository.All().FirstOrDefault(x => x.DiscountType == DiscountType.VIP);

                var card = new UsersDiscountCard()
                {
                    DiscountCard = vipCard,
                    User = user,
                };

                user.DiscountCards.Add(card);
                vipCard.Users.Add(card);
                
                this.usersRepository.SaveChanges();

                if (user.DiscountCards.Any() && user.DiscountCards.Any(x => x.DiscountCard.DiscountType == DiscountType.VIP))
                {
                    var discoutPercentage = user.DiscountCards
                        .Where(x => x.DiscountCard.DiscountType == DiscountType.VIP)
                        .Select(x => x.DiscountCard.DiscountPercentage)
                        .FirstOrDefault();


                    var discount = total * (discoutPercentage / 100);
                    discountDifference = discount;
                    total -= discount;
                }

            }

            if (total >= 100 && total <= 250 && user.DiscountCards.Count == 0)
            {

                var normalCard = this.cardRepository.All().FirstOrDefault(x => x.DiscountType == DiscountType.Normal);

                var card = new UsersDiscountCard()
                {
                    DiscountCard = normalCard,
                    User = user,
                };

                user.DiscountCards.Add(card);
                normalCard.Users.Add(card);

                this.usersRepository.SaveChanges();

                if (user.DiscountCards.Any() && user.DiscountCards.Any(x => x.DiscountCard.DiscountType == DiscountType.Normal))
                {
                    var discoutPercentage = user.DiscountCards
                        .Where(x => x.DiscountCard.DiscountType == DiscountType.Normal)
                        .Select(x => x.DiscountCard.DiscountPercentage)
                        .FirstOrDefault();

                    var discount = total * (discoutPercentage / 100);
                    discountDifference = discount;
                    total -= discount;
                }
            }

           

            total = Math.Round(total, 2);
            discountDifference = Math.Round(discountDifference, 2);


            return new AllMyOrdersViewModel()
            {
                DrinksOrders = drinksOrders,
                DishesOrders = dishesOrders,
                Total = total,
                Discount = discountDifference,
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
