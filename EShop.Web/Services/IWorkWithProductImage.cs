namespace EShop.Web.Services
{
    public interface IWorkWithProductImage
    {
        string Upload(IFormFile file, int id = 0);
    }
}
