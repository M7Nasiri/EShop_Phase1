using EShop.Domain.Entities;
using EShop.Domain.ViewModels.Checkout;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Interfaces
{
    public interface ICheckoutService
    {
        Checkout AddUserCartItemToCheckout(List<UserCartItem> items);
    }
}
