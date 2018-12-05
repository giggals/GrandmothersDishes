using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class DiscountCard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public decimal DiscountPercentage { get; set; }

        public DiscountType DiscountType { get; set; }

        public GrandMothersUser User { get; set; }
        
    }
}
