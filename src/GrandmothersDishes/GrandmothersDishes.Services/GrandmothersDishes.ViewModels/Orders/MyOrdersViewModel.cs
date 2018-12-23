using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders
{
    public class MyOrdersViewModel 
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
    }
}
