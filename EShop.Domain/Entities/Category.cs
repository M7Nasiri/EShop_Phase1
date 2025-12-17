using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان الزامی است")]
        [MaxLength(100)]
        [MinLength(3, ErrorMessage = "حداقل طول عنوان باید 3 باشد .")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public IList<Product>? Products { get; set; }
        public bool IsDelete { get; set; }
    }
}
