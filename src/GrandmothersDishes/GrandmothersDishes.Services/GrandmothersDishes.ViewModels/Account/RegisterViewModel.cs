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
        private const int MinLenghtPassword = 3;

        private const int MaxLenghtPassword = 30;

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match";
        
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(30, ErrorMessage = "The password must be atleast 3 characters long and max 30 characters"), MinLength(3), MaxLength(30)]
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
