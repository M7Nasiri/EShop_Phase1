using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<List<UserCartItem>> GetUserCart(int userId);
        Task<UserCartItem?> GetUserCartItem(int userId, int productId);
        Task AddItem(UserCartItem item);
        Task UpdateItem(UserCartItem item);
        Task RemoveItem(UserCartItem item);
        Task<int> GetUserCartCount(int userId);
        Task Save();

        Task AddToCart(int userId, int productId, int count = 1);
        Task<List<UserCartItem>> GetUserCartItems(int userId);
        Task RemoveFromCart(int userId, int productId);

        Task RemoveAllItemRelatedToUser(int userId);
    }

}
