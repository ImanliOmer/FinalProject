using Common.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.File
{
	public interface IFileService
	{
		string Upload(IFormFile file);
		void Delete(string photoName);

		bool IsImage(object file);

		bool IsBiggerThanSize(object file, int size = 100);
		
	}
}
