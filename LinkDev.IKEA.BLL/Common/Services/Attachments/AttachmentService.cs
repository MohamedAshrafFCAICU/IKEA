﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtension = new List<string>() { ".png", ".jpg", ".jpeg" };
      
        private const int _allowedMaxSize = 2_097_152;
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName); // ahmed.png

            if (!_allowedExtension.Contains(extension)) 
                return null;

            if (file.Length > _allowedMaxSize)
                return null;

            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            if (Directory.Exists(folderPath)) 
                Directory.CreateDirectory(folderPath);  

            var fileName = $"{Guid.NewGuid()}{extension}"; // Must Be UNIQUE

            var filePath = Path.Combine(folderPath, fileName);  // File Location Placed


            // Streaming: Data Per Time

            using var fileStream = new FileStream(filePath, FileMode.Create);

            //using var fileStream =  File.Create(filePath);

            await file.CopyToAsync(fileStream);    

            return  fileName;    
        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;

            }
            return false;
        }
    }
}
