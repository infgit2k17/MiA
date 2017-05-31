using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace MiA_projekt.Manager
{
    public static class ImageManager
    {
        public static string Save(IFormFile file, string userId)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", userId);
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

            return "/images/" + userId + "/" + file.FileName;
        }

        public static IEnumerable<string> Save(IEnumerable<IFormFile> files, string userId)
        {
            var paths = new List<string>();

            foreach (var file in files)
            {
                paths.Add(Save(file, userId));
            }

            return paths;
        }
    }
}
