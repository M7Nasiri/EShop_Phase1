namespace EShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsFinalized { get; set; }
        public long TotalCount { get; set; }
        public bool IsDelete { get; set; }

    }
}
