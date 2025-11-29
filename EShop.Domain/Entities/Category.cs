namespace EShop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public IList<Product>? Products { get; set; }
        public bool IsDelete { get; set; }
    }
}
