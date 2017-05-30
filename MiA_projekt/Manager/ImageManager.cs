using Microsoft.AspNetCore.Http;
using System.IO;

namespace MiA_projekt.Manager
{
    public static class ImageManager
    {
        public static string Save(IFormFile file, string userId)
        {
            var directoryPath = Path.Combine("D:\\mia-images\\", userId);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, file.FileName);

            if (file.Length > 0)
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return filePath;
        }
    }
}
