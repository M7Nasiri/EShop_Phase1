using AutoMapper;
using EShop.Domain.Dtos.OrderAgg;
using EShop.Domain.Entities;
using EShop.Domain.Enum;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class OrderRepository(AppDbContext _context,IMapper _mapper) : IOrderRepository
    {
        public async Task<int> CreateOrder(int userId, List<UserCartItem> items)
        {
            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                Status = OrderStatus.Pending,
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

        public async Task<bool> ShippededOrder(int orderId)
        {
            return await _context.Orders.Where(o => o.Id == orderId && o.IsFinalized).ExecuteUpdateAsync(setters => setters
            .SetProperty((o => o.Status), OrderStatus.Shipped)) > 0;
        }

        public async Task FinalizedOrder(int orderId)
        {
            await _context.Orders.Where(o => o.Id == orderId).ExecuteUpdateAsync(setters => setters
            .SetProperty((o => o.IsFinalized), true)
            .SetProperty((o => o.Status), OrderStatus.Paid));
        }

        public async Task<GetOrderDto> Get(int id)
        {
            return  _mapper.Map<GetOrderDto>(await _context.Orders.Include(o => o.User).Include(o => o.OrderItems)
                .ThenInclude(o => o.Product).Where(o => o.Id == id).FirstOrDefaultAsync());
        }

        public async Task<List<GetOrderDto>> GetAll()
        {
            return _mapper.Map<List<GetOrderDto>>(await _context.Orders.Include(o=>o.User).Include(o => o.OrderItems)
                .ThenInclude(o => o.Product).ToListAsync());
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(o => o.User).Include(o=>o.OrderItems).ThenInclude(o=>o.Product).Where(o => o.Id == orderId).FirstOrDefaultAsync();
        }


    }
}
