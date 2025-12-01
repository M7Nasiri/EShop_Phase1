using EShop.Domain.Entities;

namespace EShop.Application.Dtos
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public long UnitPrice { get; set; }
    }

}


