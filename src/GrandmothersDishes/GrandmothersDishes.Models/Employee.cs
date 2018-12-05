using GrandmothersDishes.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Models
{
    public class Employee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public int WorkingHours { get; set; }

        public EmloyeeType EmloyeeType { get; set; }

        public Gender Gender { get; set; }
        
    }
}
