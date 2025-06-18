using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.Utilities;

namespace Infrastructure.Utilities
{
    public class CloudinaryVideoService:IVideoService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryVideoService(IConfiguration config)
        {
            var cloudName = config["Cloudinary:CloudName"];
            var apiKey = config["Cloudinary:ApiKey"];
            var apiSecret = config["Cloudinary:ApiSecret"];

            Account account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadVideoAsync(Stream fileStream, string fileName)
        {
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                Folder = "course_videos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }


    }
}
