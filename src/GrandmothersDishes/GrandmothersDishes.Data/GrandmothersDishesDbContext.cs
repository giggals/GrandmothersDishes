using GrandmothersDishes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GrandmothersDishes.Data
{
    public class GrandmothersDishesDbContext : IdentityDbContext<GrandMothersUser>
    {
        public GrandmothersDishesDbContext(DbContextOptions<GrandmothersDishesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<DiscountCard> DiscountCards { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDishes> OrderDishes { get; set; }

        public DbSet<OrderDrinks> OrderDrinks { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }




    }
}
