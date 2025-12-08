using EShop.Domain.Dtos.Checkout;
using EShop.Domain.Entities;

namespace EShop.Application.Interfaces
{
    public interface ICheckoutService
    {
        Checkout AddUserCartItemToCheckout(List<UserCartItem> items);
    }
}
