using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class FileUploadHelper
    {
        IHostingEnvironment env;

        public FileUploadHelper(IHostingEnvironment env)
        {
            this.env = env;
        }
        public async Task<bool> UploadFileAsync(Carousel carousel)
        {
            bool uploadSuccess = false;
            var img = carousel.Image;
            
            carousel.ImageSrc = String.Empty;

            if (img != null && img.ContentType.ToLower().Contains("image") && img.Length > 0 && img.Length < 2_000_000)
            {
                string fileNameWithExtension = (Path.GetFileNameWithoutExtension(img.FileName) + Path.GetExtension(img.FileName));
                //TODO: osetrit prepsani stejnojmenneho souboru
                //var filinameGenerated = Path.GetRandomFileName();

                var filePathRelative = Path.Combine("images", "Carousels", fileNameWithExtension);
                var filePath = Path.Combine(env.WebRootPath, "images", "Carousels", fileNameWithExtension);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                carousel.ImageSrc = $"\\{filePathRelative}";

                uploadSuccess = true;
            }

            return uploadSuccess;
        }
    }
}
