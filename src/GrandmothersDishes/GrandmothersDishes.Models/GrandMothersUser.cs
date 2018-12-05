using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class GrandMothersUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string HomeAddress { get; set; }

        public DateTime DateOfBirth { get; set; }
        
    }
}
