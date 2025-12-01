using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserCartItem>> GetUserCart(int userId)
        {
            return await _context.UserCartItems
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserCartItem?> GetUserCartItem(int userId, int productId)
        {
            return await _context.UserCartItems
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
        }

        public async Task AddItem(UserCartItem item)
        {
            await _context.UserCartItems.AddAsync(item);
        }

        public async Task UpdateItem(UserCartItem item)
        {
            _context.UserCartItems.Update(item);
        }

        public async Task RemoveItem(UserCartItem item)
        {
            _context.UserCartItems.Remove(item);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserCartCount(int userId)
        {
            //return await _context.UserCartItems
            //.Where(x => x.UserId == userId)
            //.SumAsync(x => x.Count);
            return await _context.UserCartItems
        .Where(x => x.UserId == userId)
        .CountAsync();
        }



        public async Task AddToCart(int userId, int productId, int count = 1)
        {
            // بررسی رکورد موجود
            var existing = await _context.UserCartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (existing != null)
            {
                existing.Count += count;
                _context.UserCartItems.Update(existing);
            }
            else
            {
                var newItem = new UserCartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = count
                };
                await _context.UserCartItems.AddAsync(newItem);
            }
            await _context.SaveChangesAsync();
        }





        //public async Task AddToCart(int userId, int productId, int count = 1)
        //{
        //    var cartItem = await _context.UserCartItems
        //        .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        //    if (cartItem != null)
        //    {
        //        cartItem.Count += count;
        //    }
        //    else
        //    {
        //        cartItem = new UserCartItem
        //        {
        //            UserId = userId,
        //            ProductId = productId,
        //            Count = count
        //        };
        //        await _context.UserCartItems.AddAsync(cartItem);
        //    }
        //    await _context.SaveChangesAsync();
        //}
        public async Task<List<UserCartItem>> GetUserCartItems(int userId)
        {
            return await _context.UserCartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
        }
        public async Task RemoveFromCart(int userId, int productId)
        {
            var cartItem = await _context.UserCartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.UserCartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteItem(UserCartItem item)
        {
            _context.UserCartItems.Remove(item);
        }

        public async Task RemoveAllItemRelatedToUser(int userId)
        {
            var items = _context.UserCartItems.Where(uci=>uci.UserId == userId);
            _context.UserCartItems.RemoveRange(items);
        }
    }

}
