namespace EShop.Domain.Dtos.Checkout
{
    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public long LineTotal => Price * Quantity;
    }
}
