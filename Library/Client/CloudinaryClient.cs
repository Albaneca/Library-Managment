using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Library.API.Client.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Library.API.Client
{
    public class CloudinaryClient
    {
        private readonly Cloudinary cloudinary;

        private readonly CloudinaryConfig config;
        public CloudinaryClient(CloudinaryConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.cloudinary = new Cloudinary(new Account
            {
                ApiKey = this.config.ApiKey,
                ApiSecret = this.config.ApiSecret,
                Cloud = this.config.Cloud
            });
        }

        public async Task<ImageUploadResult> UploadProfilePhotoAsync(IFormFile file)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                Folder = "profilePictures",
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }
        public async Task<ImageUploadResult> UploadBookPhotoAsync(IFormFile file)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                Folder = "bookPictures",
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }

        public async Task<ImageUploadResult> UploadAuthorPhotoAsync(IFormFile file)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                Folder = "authorPictures",
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }

        public async Task<ImageUploadResult> UploadHousePhotoAsync(IFormFile file)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                Folder = "publishHousePictures",
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }
    }

}
