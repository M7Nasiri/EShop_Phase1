using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Interfaces
{
    public interface ICartService
    {
        Task<List<UserCartItem>> GetUserCart(int userId);
        Task<UserCartItem?> GetUserCartItem(int userId, int productId);
        Task AddItem(UserCartItem item);
        Task RemoveItem(UserCartItem item);
        Task MergeGuestCartIntoUserCart(int userId, List<GuestCartItem> guestItems);
        Task AddToUserCart(int userId, int productId);
        Task<int> GetUserCartCount(int userId);
        Task<List<UserCartItem>> GetUserCartItems(int userId);
        Task RemoveFromUserCart(int userId, int productId);
        Task DeleteFromUserCart(int userId, int productId);
        Task RemoveAllItemRelatedToUser(int userId);
    }
}
