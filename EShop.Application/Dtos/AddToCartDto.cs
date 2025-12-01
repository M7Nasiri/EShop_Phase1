namespace EShop.Application.Dtos
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public string? GuestId { get; set; } 
    }

}
