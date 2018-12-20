using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Employees
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [StringLength(EmployeeConstants.MaxFirstNameLenght, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = EmployeeConstants.MinFirstNameLenght)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EmployeeConstants.MaxLastNameLenght, ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage, MinimumLength = EmployeeConstants.MinLastNameLenght)]
        public string LastName { get; set; }

        [Required]
        [Range(typeof(decimal), EmployeeConstants.MinEmployeeSalaryAsString, EmployeeConstants.MaxEmployeeSalaryAsString)]
        public decimal Salary { get; set; }

        [Required]
        [Range(EmployeeConstants.MinWorokingHours,EmployeeConstants.MaxWorokingHours)]
        public int WorkingHours { get; set; }

        [Required]
        public string EmployeeType { get; set; }
        
        [Required]
        public string Gender { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        
    }
}
