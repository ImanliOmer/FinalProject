using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Business.Utilities.File;
using Common.Entities;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Business.Utilities.File
{
	public class FileService : IFileService
	{

		private readonly IHostingEnvironment _webHostEnvironment;

		public FileService(IHostingEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public bool IsBiggerThanSize(object file, int size = 100)
		{

			var list = new List<IFormFile>();

			if (file is List<IFormFile>)
			{
				list = file as List<IFormFile>;
			}
			else if (file is IFormFile)
			{
				list.Add(file as IFormFile);
			}

            foreach (var img in list)
            {
                if (img.Length / 1024 > size)
                {
					return true;
                }
            }

            return false;
		}

		public void Delete(string photoName)
		{
			var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "User/assets/images", photoName);
			


			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);
		}

		public bool IsImage(object file)
		{
			var list = new List<IFormFile>();

            if (file is List<IFormFile>)
            {
                list = file as List<IFormFile>;
            }
            else if(file is IFormFile)
            {
				list.Add(file as IFormFile);
			}
            foreach (var img in list)
            {
                if (img.ContentType != "image/png" && img.ContentType != "image/jpeg")
                {
					return false;
                }
            }
			return true;
		}

		public string Upload(IFormFile file)
		{
			var fileName = Guid.NewGuid() + "_" + file.FileName;
			var path = Path.Combine(_webHostEnvironment.WebRootPath, "User/assets/images", fileName);

			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
				file.CopyTo(fileStream);

			return fileName;
		}

		
	}
}
