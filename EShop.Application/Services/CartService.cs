using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EShop.Application.Services
{
    public class CartService(ICartRepository _cartRepository) : ICartService
    {

        public async Task AddItem(UserCartItem item)
        {
            await _cartRepository.AddItem(item);
            await _cartRepository.Save();
        }

        public async Task<List<UserCartItem>> GetUserCart(int userId)
        {
            return await _cartRepository.GetUserCart(userId);
        }

        public async Task<UserCartItem?> GetUserCartItem(int userId, int productId)
        {
            return await _cartRepository.GetUserCartItem(userId, productId);
        }

        public async Task MergeGuestCartIntoUserCart(int userId, List<GuestCartItem> guestItems)
        {
            if (guestItems == null || guestItems.Count == 0) return;

            // دریافت تمام آیتم‌ها یکجا برای جلوگیری از concurrency
            var userCartItems = await _cartRepository.GetUserCart(userId);

            foreach (var g in guestItems)
            {
                var existing = userCartItems.FirstOrDefault(x => x.ProductId == g.productId);
                if (existing != null)
                {
                    existing.Count += g.quantity;
                    await _cartRepository.UpdateItem(existing);
                }
                else
                {
                    await _cartRepository.AddItem(new UserCartItem
                    {
                        UserId = userId,
                        ProductId = g.productId,
                        Count = g.quantity
                    });
                }
            }

            await _cartRepository.Save();
        }

        public async Task RemoveItem(UserCartItem item)
        {
            await _cartRepository.RemoveItem(item);
            await _cartRepository.Save();
        }

        public async Task AddToUserCart(int userId, int productId)
        {
            await _cartRepository.AddToCart(userId, productId, 1);
        }

        public async Task RemoveFromUserCart(int userId, int productId)
        {
            var item = await _cartRepository.GetUserCartItem(userId, productId);
            if (item != null)
            {
                if (item.Count > 1)
                {
                    item.Count--;
                    await _cartRepository.UpdateItem(item);
                }
                else
                {
                    await _cartRepository.RemoveItem(item);
                }
                await _cartRepository.Save();
            }
        }

        public async Task DeleteFromUserCart(int userId, int productId)
        {
            var item = await _cartRepository.GetUserCartItem(userId, productId);
            if (item != null)
            {
                await _cartRepository.RemoveItem(item);
                await _cartRepository.Save();
            }
        }

        public async Task RemoveAllItemRelatedToUser(int userId)
        {
            await _cartRepository.RemoveAllItemRelatedToUser(userId);
        }

        public async Task<List<UserCartItem>> GetUserCartItems(int userId)
        {
            return await _cartRepository.GetUserCartItems(userId);
        }

        public async Task<int> GetUserCartCount(int userId)
        {
            return await _cartRepository.GetUserCartCount(userId);
        }

        public async Task<long> CalculateTotal(List<UserCartItem> carts)
        {
            return carts.Sum(x => x.Count * x.Product.UnitCost);
        }


    }
}
