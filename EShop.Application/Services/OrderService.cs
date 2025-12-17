using EShop.Application.Interfaces;
using EShop.Domain.common;
using EShop.Domain.Dtos.OrderAgg;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;


namespace EShop.Application.Services
{
    public class OrderService(IOrderRepository _orderRepository,IUserService _userService
        ,ICartService _cartService,IProductService _productService)
        : IOrderService
    {
        public async Task<ResultDto<long>> CheckOrder(int userId, List<UserCartItem> cart)
        {

            var result = new ResultDto<long>();
            long totalPrice = await _cartService.CalculateTotal(cart);

            long wallet = _userService.GetUserWallet(userId);
            if (cart.Count == 0)
            {
                result.IsSuccess = false;
                result.Message = "سبد شما خالی است ";
                return result;
            }
            if (!_userService.ISCreditSufficient(wallet, totalPrice))
            {
                result.IsSuccess = false;
                result.Message = "موجودی کافی نیست";
                return result;
            }
            foreach (var c in cart)
            {
                int countInBasket = c.Count;
                var res = _productService.HasEnoughStock(c.Product.Id, countInBasket);
                if (!res.IsSuccess)
                {
                    result.IsSuccess = false;
                    result.Message = $"موجودی کالای {c.Product.Title} کمتر از میزان درخواستی شما است";
                    return result;
                }
                _productService.UpdateStock(c.Product.Id,res.Data, countInBasket);
            }

         
            result.IsSuccess = true;
            result.Data = totalPrice;
            result.Message = "خرید شما با موفقیت انجام شد. ممنون از خرید شما";
            return result;
        }
        public Task<int> CreateOrder(int userId, List<UserCartItem> items)
        {
            return _orderRepository.CreateOrder(userId, items);
        }

        public async Task FinalizedOrder(int orderId)
        {
           await _orderRepository.FinalizedOrder(orderId);
        }

        public async Task<int> Finalized(int userId, List<UserCartItem> cart, long totalPrice)
        {
            int orderId = await CreateOrder(userId, cart);

            _userService.DecreaseWallet(userId, totalPrice);
            await FinalizedOrder(orderId);

            await _cartService.RemoveAllItemRelatedToUser(userId);
            return orderId;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public async Task<bool> ShippededOrder(int orderId)
        {
            var res =  await _orderRepository.ShippededOrder(orderId);
            if (!res)
            {
                throw new Exception("برای ارسال محصول ، پرداخت باید انجام شده باشد");
            }
            return res;
        }

        public async Task<List<GetOrderDto>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<GetOrderDto> Get(int id)
        {
            return await _orderRepository.Get(id);
        }

        public async Task<List<GetOrderDto>> GetUserOrders(int userId)
        {
            return await _orderRepository.GetUserOrders(userId);
        }
    }
}
