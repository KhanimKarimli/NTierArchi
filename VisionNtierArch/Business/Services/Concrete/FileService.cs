using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Concrete
{
    public class FileService:IFileService
    {
		private readonly string _rootPath;
		private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
		private const long _maxFileSize = 2 * 1024 * 1024;

		public FileService(IWebHostEnvironment env)
		{
			_rootPath = Path.Combine(env.WebRootPath, "uploads");
		}

		public async Task<string> UploadAsync(IFormFile file, string folderName)
		{
			if (file == null || file.Length == 0)
				throw new Exception("File boşdur");

			if (file.Length > _maxFileSize)
				throw new Exception("File ölçüsü 2MB-dan böyükdür");

			var extension = Path.GetExtension(file.FileName).ToLower();

			if (!_allowedExtensions.Contains(extension))
				throw new Exception("Yalnız şəkil formatları icazəlidir");

			var fileName = Guid.NewGuid() + extension;

			var folderPath = Path.Combine(_rootPath, folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fullPath = Path.Combine(folderPath, fileName);

			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return $"/Images/{folderName}/{fileName}";
		}

		public void Delete(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				return;

			var fullPath = Path.Combine(_rootPath, filePath.Replace("/uploads/", ""));
			if (File.Exists(fullPath))
				File.Delete(fullPath);
		}
	}
}
