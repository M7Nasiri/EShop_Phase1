using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Interfaces
{
    public interface IFileService
    {
        public string Upload(IFormFile file, string folder);
        public void Delete(string fileName);
    }
}
