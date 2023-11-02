using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.Product;
using Business.ViewModels.Admin.ProductCategory;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concret.Admin
{
	public class ProductCategoryService : IProductCategoryService
	{

		private readonly IProductCategoryRepository _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFileService _fileService;
		private readonly IProductRepository _productRepository;
		private readonly ModelStateDictionary _modelState;

		public ProductCategoryService(IProductCategoryRepository categoryRepository,
										IActionContextAccessor contextAccessor,
										IFileService fileService,
										IUnitOfWork unitOfWork,
										IProductRepository productRepository)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
			_fileService = fileService;
			_productRepository = productRepository;
			_modelState = contextAccessor.ActionContext.ModelState;
		}




		public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetByNameAsync(model.CategoryName);
			if (category is not null)
			{
				_modelState.AddModelError("CategoryName", "Category under this name already exists in database");
				return false;
			}

			category = new ProductCategory
			{
				CategoryName = model.CategoryName,
				ParentCategoryId = model.ParentId,
				CreatedAt = DateTime.Now,
			};
			if (model.Photo != null)
			{
				if (!_fileService.IsImage(model.Photo))
				{
					_modelState.AddModelError("Photo", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.Photo, 200))
				{
					_modelState.AddModelError("Photo", "File size is over 200kb");
					return false;
				}

				category.Photo = _fileService.Upload(model.Photo);

			}






			await _categoryRepository.CreateAsync(category);
			await _unitOfWork.CommitAsync();
			return true;
		}


		public async Task<ProductCategoryCreateVM> CreateCategoryCreateVMAsync()
		{
			var model = new ProductCategoryCreateVM()
			{
				Children = await _categoryRepository.GetAll(),
			};
			return model;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var category = await _categoryRepository.GetById_IncludeChildren(id);
			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return false;
			}

			foreach (var childe in category.Children)
			{
				if (category is not null)
				{
					_categoryRepository.SoftDelete(category);
				}
			}

			if (category.Photo is not null)
			{
                _fileService.Delete(category.Photo);
            }
			_categoryRepository.SoftDelete(category);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<ProducCategorytListVM> GetAllAsync()
		{
			var model = new ProducCategorytListVM
			{
				ProductCategories = await _categoryRepository.GetProductsWithPhotos()
			};
			return model;
		}

		public async Task<ProductCategoryUpdateVM> UpdateAsync(int id)
		{
			var category = await _categoryRepository.GetById(id);
			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return null;
			}
			
			

			var model = new ProductCategoryUpdateVM
			{
				CategoryName = category.CategoryName,
				ParentId = category.ParentCategoryId,
				Photo = category.Photo,
				Children = await _categoryRepository.GetAll(),
			};
			return model;
		}

		public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetById(id);


			

			if (model.ParentId == category.Id)
			{
				_modelState.AddModelError("ParenId", "Chose another category");
				return false;
			}

			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return false;
			}

			category.CategoryName = model.CategoryName;
			category.ParentCategoryId = model.ParentId;
			category.ModfiedAt = DateTime.Now;
			if (model.NewPhoto != null)
			{
				if (!_fileService.IsImage(model.NewPhoto))
				{
					_modelState.AddModelError("NewPhoto", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.NewPhoto, 200))
				{
					_modelState.AddModelError("NewPhoto", "File size is over 200kb");
					return false;
				}

				if (!string.IsNullOrEmpty(category.Photo))
				{
					_fileService.Delete(category.Photo);
				}
				category.Photo = _fileService.Upload(model.NewPhoto);
			}

			_categoryRepository.Update(category);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
