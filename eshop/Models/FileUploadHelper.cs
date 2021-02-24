using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class FileUploadHelper
    {
        string RootPath;
        string ContentType;
        string DirName;

        public FileUploadHelper(string rootPath, string dirName, string contentType)
        {
            RootPath = rootPath;
            DirName = dirName;
            ContentType = contentType;
        }

        public bool ContentCheck(IFormFile iFormFile)
        {
            return iFormFile != null && iFormFile.ContentType.ToLower().Contains(ContentType);
        }

        public bool LenghtCheck(IFormFile iFormFile)
        {
            return iFormFile.Length > 0 && iFormFile.Length < 2_000_000;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string uploadedFilePath = String.Empty;

            if (ContentCheck(file) && LenghtCheck(file))
            {
                string fileNameWithExtension = (Path.GetFileNameWithoutExtension(file.FileName) + Path.GetExtension(file.FileName));
                //TODO: osetrit prepsani stejnojmenneho souboru
                //var filinameGenerated = Path.GetRandomFileName();

                var filePathRelative = Path.Combine(ContentType + "s", DirName, fileNameWithExtension);
                var filePath = Path.Combine(RootPath, filePathRelative);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                uploadedFilePath = $"\\{filePathRelative}";

            }

            return uploadedFilePath;
        }
    }
}
