using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {
        public async Task<int> CreateOrder(int userId, List<UserCartItem> items)
        {
            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                TotalPrice = items.Sum(x => x.Count * x.Product.UnitCost)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var i in items)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = i.ProductId,
                    Amount = i.Count,
                    UnitPrice = i.Product.UnitCost
                });
            }

            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task FinalizedOrder(int orderId)
        {
            _context.Orders.Where(o => o.Id == orderId).ExecuteUpdate(setters => setters.SetProperty((o => o.IsFinalized), true));
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(o=>o.OrderItems).ThenInclude(o=>o.Product).Where(o => o.Id == orderId).FirstOrDefaultAsync();
        }
    }
}
