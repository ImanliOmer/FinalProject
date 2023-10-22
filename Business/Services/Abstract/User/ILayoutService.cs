using Business.ViewModels.Admin.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
	public interface ILayoutService
	{
		Task<ProducCategorytListVM> GetCategories();
		string test();
	}
}
