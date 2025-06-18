using System;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Domain.Interfaces.Utilities;

namespace Infrastructure.Utilities
{
    // Infrastructure.Services
    public class LocalImageStorageService : IImageStorageService
    {
        private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<string> SaveImageAsync(Stream imageStream)
        {
            if (imageStream == null || imageStream.Length == 0)
                return null;

            // Detect image format and get appropriate extension
            string fileExtension = GetImageExtension(imageStream);
            string fileName = $"{GetGuid()}{fileExtension}";

            Directory.CreateDirectory(_imageFolder);

            var filePath = Path.Combine(_imageFolder, fileName);

            // Reset position in case we read from the stream for detection
            imageStream.Position = 0;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            string baseUrl = "https://localhost:7124"; // Adjust this as needed
            return $"{baseUrl}/images/{fileName}";
        }

        private string GetImageExtension(Stream stream)
        {
            // Read the first few bytes to detect the signature
            var buffer = new byte[8];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            stream.Position = 0; // Reset position after reading

            if (bytesRead < 2) return ".dat"; // fallback

            // Check for common image formats
            if (buffer[0] == 0xFF && buffer[1] == 0xD8) return ".jpg";
            if (buffer[0] == 0x89 && buffer[1] == 0x50 &&
                buffer[2] == 0x4E && buffer[3] == 0x47) return ".png";
            if (buffer[0] == 0x47 && buffer[1] == 0x49 &&
                buffer[2] == 0x46) return ".gif";
            if (buffer[0] == 0x42 && buffer[1] == 0x4D) return ".bmp";
            if (buffer[0] == 0x52 && buffer[1] == 0x49 &&
                buffer[2] == 0x46 && buffer[3] == 0x46) return ".webp";

            return ".dat"; // default fallback
        }

    }

}
