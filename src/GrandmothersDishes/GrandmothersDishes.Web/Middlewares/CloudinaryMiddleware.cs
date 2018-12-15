using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace GrandmothersDishes.Web.Middlewares
{
    public class CloudinaryMiddleware
    {
        private readonly RequestDelegate next;

        public CloudinaryMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        
        private static void ClodunaryConfiguration()
        {
              Account account = new Account(
                "dufibvs7r",
                "465524985188794",
                "QRbkkcT1yjionsd8y89s7W9sNKU");

             CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"C:\Users\nikol\Desktop\GrandmothersDishes_files\navbarIcons\chek.png"),
                PublicId = "chek",

            };

            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
        }


        public async Task InvokeAsync(
            HttpContext context)
        {
            ClodunaryConfiguration();

            await this.next(context);
        }
    }
}
