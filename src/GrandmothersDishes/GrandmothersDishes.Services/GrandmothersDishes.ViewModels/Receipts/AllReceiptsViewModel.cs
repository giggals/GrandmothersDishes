using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Receipts
{
    public class AllReceiptsViewModel
    {
        public ICollection<ReceiptViewModel> Receipts { get; set; }

        public decimal Total { get; set; }

    }
}
