using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Receipts
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        
        public DateTime IssuedOn { get; set; }

        public string Cashier { get; set; }
    }
}
