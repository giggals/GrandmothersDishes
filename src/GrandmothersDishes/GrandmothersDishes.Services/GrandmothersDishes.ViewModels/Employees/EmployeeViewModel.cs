using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public int WorkingHours { get; set; }

        public string Gender { get; set; }

        public string EmployeeType  { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
