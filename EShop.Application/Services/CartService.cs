using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace EShop.Application.Services
{
    public class CartService(ICartRepository cartRepository) : ICartService
    {
        public async Task AddItem(UserCartItem item)
        {
            await cartRepository.AddItem(item);
        }

        public async Task<List<UserCartItem>> GetUserCart(int userId)
        {
            return await cartRepository.GetUserCart(userId);    
        }
        
        public async Task<UserCartItem?> GetUserCartItem(int userId, int productId)
        {
            return await cartRepository.GetUserCartItem(userId, productId);
        }

        public async Task MergeGuestCartIntoUserCart(int userId, List<GuestCartItem> guestItems)
        {
            if (guestItems == null || guestItems.Count == 0) return;

            foreach (var g in guestItems)
            {
                var existing = await cartRepository.GetUserCartItem(userId, g.ProductId);

                if (existing == null)
                {
                    await cartRepository.AddItem(new UserCartItem
                    {
                        UserId = userId,
                        ProductId = g.ProductId,
                        Count = g.Count
                    });
                }
                else
                {
                    existing.Count += g.Count;
                    await cartRepository.UpdateItem(existing);
                }
            }

            await cartRepository.Save();
        }

        public async Task RemoveItem(UserCartItem item)
        {
            await cartRepository.RemoveItem(item);
        }

        public async Task Save()
        {
            await cartRepository.Save();
        }

        public async Task UpdateItem(UserCartItem item)
        {
            await cartRepository.UpdateItem(item);
            await cartRepository.Save();
        }
        public async Task AddToUserCart(int userId, int productId)
        {
            var item = await cartRepository.GetUserCartItem(userId, productId);

            if (item == null)
            {
                await cartRepository.AddItem(new UserCartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = 1
                });
            }
            else
            {
                item.Count++;
                await cartRepository.UpdateItem(item);
            }

            await cartRepository.Save();
        }

        public async Task<int> GetUserCartCount(int userId)
        {
            return await cartRepository.GetUserCartCount(userId);
        }

        public async Task AddToCart(int userId, int productId, int count = 1)
        {
            await cartRepository.AddToCart(userId, productId, count); 
        }

        public async Task<List<UserCartItem>> GetUserCartItems(int userId)
        {
            return await cartRepository.GetUserCartItems(userId);
        }

        public async Task RemoveFromCart(int userId, int productId)
        {
            await cartRepository.RemoveFromCart(userId, productId);
        }

        public async Task RemoveFromUserCart(int userId, int productId)
        {
            var existing = await cartRepository.GetUserCartItem(userId, productId);
            if (existing != null)
            {
                existing.Count--;
                if (existing.Count <= 0)
                {
                    await cartRepository.RemoveItem(existing);
                }
                else
                {
                    await cartRepository.UpdateItem(existing);
                }
                await cartRepository.Save();
            }
        }
        public async Task DeleteFromUserCart(int userId, int productId)
        {
            var existing = await cartRepository.GetUserCartItem(userId, productId);
            if (existing != null)
            {
                await cartRepository.RemoveItem(existing);
                await cartRepository.Save();
            }
        }

        public async Task RemoveAllItemRelatedToUser(int userId)
        {
            await cartRepository.RemoveAllItemRelatedToUser(userId);
            await cartRepository.Save();
        }
    }
}
