using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Web.ViewModels.Checkout
{
    public class Checkout
    {
        public List<CheckoutItem> Items { get; set; } = new();
        public long TotalPrice { get; set; }
    }
}
