using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers
{
    public class DeliveryDetailsViewModel
    {
        public string Id { get; set; }

        public string DeliveredOn { get; set; }

        public string DeliveryType { get; set; }

        public string Address { get; set; }

        public string User { get; set; }

        public int TimeToDeliver { get; set; }
    }
}
