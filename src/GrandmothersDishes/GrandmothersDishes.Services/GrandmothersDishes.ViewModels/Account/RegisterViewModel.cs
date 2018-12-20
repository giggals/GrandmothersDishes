using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.Constants;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;

namespace GrandmothersDishes.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(AccountConstants.MaxPasswordLenght, ErrorMessage = AccountConstants.PasswordErrorMessage, MinimumLength = AccountConstants.MinPasswordLenght)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = AccountConstants.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
