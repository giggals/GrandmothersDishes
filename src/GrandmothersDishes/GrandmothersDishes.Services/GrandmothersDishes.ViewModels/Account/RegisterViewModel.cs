using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Models;
using GrandmothersDishes.Services.GrandmothersDishes.Mapping.Service;

namespace GrandmothersDishes.Web.ViewModels.Account
{
    public class RegisterViewModel : IMapFrom<GrandMothersUser>
    {

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match";

        private const string PasswordErrorMessage =
            "The password must be atleast {2} characters long and max {3} characters";


        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(30, ErrorMessage = PasswordErrorMessage, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = ConfirmPasswordErrorMessage)]
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
