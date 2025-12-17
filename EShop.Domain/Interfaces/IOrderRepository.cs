using EShop.Domain.Dtos.OrderAgg;
using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(int userId, List<UserCartItem> items);
        Task FinalizedOrder(int orderId);

        Task<Order> GetOrderById(int orderId);

        Task<bool> ShippededOrder(int orderId);

        Task<List<GetOrderDto>> GetAll();
        Task<GetOrderDto> Get(int id);

        Task<List<GetOrderDto>> GetUserOrders(int userId);
    }
}
