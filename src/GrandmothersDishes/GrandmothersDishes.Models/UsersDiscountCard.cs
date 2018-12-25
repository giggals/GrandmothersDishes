using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class UsersDiscountCard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public GrandMothersUser User { get; set; }

        public DiscountCard DiscountCard { get; set; }
    }
}

