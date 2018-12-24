using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GrandmothersDishes.Services.Constants;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Delivers
{
    public class DeliverViewModel
    {
        [Required]
        [StringLength(DeliverConstants.MaxAddresLenght ,ErrorMessage = GlobalConstants.CharactersLenghtErrorMessage ,MinimumLength = DeliverConstants.MInAddressLenght)]
        public string Address { get; set; }

        [Required]
        public string DeliveryType { get; set; }
        
    }
}
