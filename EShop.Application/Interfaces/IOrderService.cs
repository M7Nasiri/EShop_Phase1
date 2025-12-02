using EShop.Domain.common;
using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrder(int userId, List<UserCartItem> items);
        Task FinalizedOrder(int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<ResultDto<long>> CheckOrder(int userId, List<UserCartItem> cart);
        Task<int> Finalized(int userId, List<UserCartItem> cart, long totalPrice);
    }
}
