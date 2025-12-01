namespace EShop.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public long UnitPrice { get; set; }
    }
}
