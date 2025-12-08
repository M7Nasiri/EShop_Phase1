using EShop.Application.Interfaces;
using EShop.Application.Services;

namespace EShop.Web.Services
{
    public class WorkWithProductImage : IWorkWithProductImage
    {
        private readonly IFileService _fileService;
        private readonly IProductService _productService;
        public WorkWithProductImage(IFileService fileService,IProductService productService)
        {
            _fileService = fileService;
            _productService = productService;
        }
        public string Upload(IFormFile file, int id = 0)
        {
            var path = "";
            if(file != null)
            {
                path = _fileService.Upload(file, "Products");
            }
            if(string.IsNullOrEmpty(path) && id != 0)
            {
                var imagePath = _productService.GetImagePath(id);
                path = imagePath;
            }
            return path;
        }
    }
}
