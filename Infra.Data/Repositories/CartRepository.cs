using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.Data.SqlClient;
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
                .AsNoTracking()
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

        public Task UpdateItem(UserCartItem item)
        {
            _context.UserCartItems.Update(item);
            return Task.CompletedTask;
        }

        public async Task RemoveItem(UserCartItem item)
        {
            await _context.UserCartItems.Where(x=>x.UserId ==item.UserId && x.ProductId == item.ProductId).ExecuteDeleteAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserCartCount(int userId)
        {
            return await _context.UserCartItems
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .CountAsync();
        }

        //public async Task AddToCart(int userId, int productId, int count = 1)
        //{
        //    // دریافت همه آیتم‌ها از DB یکجا
        //    var existing = await _context.UserCartItems
        //        .Where(c => c.UserId == userId && c.ProductId == productId)
        //        .FirstOrDefaultAsync();

        //    if (existing != null)
        //    {
        //        _context.UserCartItems.Where(x=>x.UserId == userId && x.ProductId ==  productId).ExecuteUpdate(
        //            setters=>setters.SetProperty((x=>x.Count), existing.Count + count));
        //    }
        //    else
        //    {
        //        var newItem = new UserCartItem
        //        {
        //            UserId = userId,
        //            ProductId = productId,
        //            Count = count
        //        };
        //        await _context.UserCartItems.AddAsync(newItem);
        //    }

        //    await _context.SaveChangesAsync();
        //}

        //public async Task AddToCart(int userId, int productId, int count = 1)
        //{
        //    var rowsAffected = await _context.UserCartItems
        //.Where(c => c.UserId == userId && c.ProductId == productId)
        //.ExecuteUpdateAsync(s => s.SetProperty(c => c.Count, c => c.Count + count));

        //    if (rowsAffected == 0)
        //    {
        //        // هیچ رکوردی پیدا نشد، پس اضافه می‌کنیم
        //        var newItem = new UserCartItem
        //        {
        //            UserId = userId,
        //            ProductId = productId,
        //            Count = count
        //        };
        //        await _context.UserCartItems.AddAsync(newItem);
        //        await _context.SaveChangesAsync();
        //    }
        //}
        private static readonly Dictionary<int, SemaphoreSlim> _locks = new();

        public async Task AddToCart(int userId, int productId, int count = 1)
        {
            SemaphoreSlim sem;
            lock (_locks)
            {
                if (!_locks.ContainsKey(userId))
                    _locks[userId] = new SemaphoreSlim(1, 1);
                sem = _locks[userId];
            }

            await sem.WaitAsync();
            try
            {
                var existing = await _context.UserCartItems
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (existing != null)
                {
                    existing.Count += count;
                }
                else
                {
                    await _context.UserCartItems.AddAsync(new UserCartItem
                    {
                        UserId = userId,
                        ProductId = productId,
                        Count = count
                    });
                }

                await _context.SaveChangesAsync();
            }
            finally
            {
                sem.Release();
            }
        }



        public async Task<List<UserCartItem>> GetUserCartItems(int userId)
        {
            return await _context.UserCartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .AsNoTracking()
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

        public async Task RemoveAllItemRelatedToUser(int userId)
        {
            var items = _context.UserCartItems.Where(c => c.UserId == userId);
            _context.UserCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }

}
