using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Orders
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        [Required]
        [Range(OrderConstants.MinQuantity , OrderConstants.MaxQuantity)]
        public int Quantity { get; set; }
    }
}
