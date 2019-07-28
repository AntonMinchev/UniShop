using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniShop.Services
{
    public interface ICloudinaryService
    {
        string UploadPicture(IFormFile pictureFile, string fileName);
    }
}
