using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Delivery
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Address { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public GrandMothersUser User { get; set; }

        public DateTime DeliveredOn { get; set; }

        public int TimeToDeliver { get; set; }
        
    }
}
