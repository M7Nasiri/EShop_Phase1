namespace EShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public OrderItem orderItem { get; set; }
        public int OrderItemId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsFinalized { get; set; }
        public long TotalCount { get; set; }
    }
}
