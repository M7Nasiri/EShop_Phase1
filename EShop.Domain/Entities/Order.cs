namespace EShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public int? UserId { get; set; }          
        public User? User { get; set; }
        public string? GuestId { get; set; }
        public bool IsFinalized { get; set; }
        public long TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }

    }
}
