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
        Task UpdateItem(UserCartItem item);
        Task RemoveItem(UserCartItem item);
        Task Save();
        Task MergeGuestCartIntoUserCart(int userId, List<GuestCartItem> guestItems);
        Task AddToUserCart(int userId, int productId);
        Task<int> GetUserCartCount(int userId);
        Task AddToCart(int userId, int productId, int count = 1);
        Task<List<UserCartItem>> GetUserCartItems(int userId);
        Task RemoveFromCart(int userId, int productId);
        Task DeleteFromUserCart(int userId, int productId);
        Task RemoveAllItemRelatedToUser(int userId);
    }
}
