using Business.Services.Abstract.User;
using Business.ViewModels.Admin.ProductCategory;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concret.Users
{
	public class LayoutService : ILayoutService
	{
		private readonly IProductCategoryRepository _productCategoryRepository;

		public LayoutService(IProductCategoryRepository productCategoryRepository)
        {
			_productCategoryRepository = productCategoryRepository;
		}
        public async Task<ProducCategorytListVM> GetCategories()
		{
			var productCategoryListVm = new ProducCategorytListVM();

			productCategoryListVm.ProductCategories = await _productCategoryRepository.GetAllWithChildren();

			return productCategoryListVm;
		}
		public string test()
		{
			return "allah haqqi calisir";
		}
	} 
}
