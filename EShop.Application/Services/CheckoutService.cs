using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Checkout;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Services
{
    public class CheckoutService() : ICheckoutService
    {
        public Checkout AddUserCartItemToCheckout(List<UserCartItem> items)
        {
            var check = new Checkout(); ;

            foreach (var item in items)
            {
                check.Items.Add(new CheckoutItem
                {
                    ProductId = item.ProductId,
                    Title = item.Product.Title,
                    Price = item.Product.UnitCost,
                    Quantity = item.Count
                });
            }

            check.TotalPrice = check.Items.Sum(x => x.LineTotal);
            return check;
        }
        
    }
}
