using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class DiscountCard
    {
        public DiscountCard()
        {
            this.Users = new HashSet<UsersDiscountCard>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public decimal DiscountPercentage { get; set; }

        public DiscountType DiscountType { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public ICollection<UsersDiscountCard> Users { get; set; }
        
    }
}
