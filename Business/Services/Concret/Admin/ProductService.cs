using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.Product;
using Business.ViewModels.Admin.ProductCategory;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Services.Concret.Admin
{

    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary _modelState;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductPhotoRepository _productPhotoRepository;

        public ProductService(IProductRepository productRepository,
                                IProductPhotoRepository productPhotoRepository,
                                IProductCategoryRepository productCategoryRepository,
                                IFileService fileService,
                                IUnitOfWork unitOfWork,
                                IActionContextAccessor contextAccessor)
        {
            _modelState = contextAccessor.ActionContext.ModelState;
            _productPhotoRepository = productPhotoRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductListVM> GetAllAsync()
        {
            var model = new ProductListVM
            {
                Products = await _productRepository.GetProductsWithPhotos()
            };
            return model;
        }
        public async Task<ProductCreateVM> Create()
        {
            var model = new ProductCreateVM
            {
                ProductCategories = _productRepository.GetAllCategories(),
            };
            return model;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            model.ProductCategories = _productRepository.GetAllCategories();

            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photos))
            {
                _modelState.AddModelError("Photos", "Wrong file format");
                return false;
            }

            if (_fileService.IsBiggerThanSize(model.Photos, 200))
            {
                _modelState.AddModelError("Photos", "File size is over 200kb");
                return false;
            }

            var product = await _productRepository.GetByNameAsync(model.Name);
            if (product is not null)
            {
                _modelState.AddModelError("Name", "Product under this name already exists in database");
                return false;
            }

            var category = await _productRepository.GetCategoryAsync(model.CategoryId);
            if (category is null)
            {
                _modelState.AddModelError("Category", "Category under this name doesn's exits");
                return false;
            }

            product = new Product
            {
                Name = model.Name,
                Cost = model.Cost,
                Description = model.Description,
                DiscountPrice = model.DiscountPrice,
                TechnicalSpecifications = model.TechnicalSpecifications,
                MainPhoto = _fileService.Upload(model.MainPhoto),
                IsMain = model.IsMain,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
            };


            foreach (var img in model.Photos)
            {
                var prductPhoto = new ProductPhot
                {
                    PhotoName = _fileService.Upload(img)
                };
                product.ProductPhots.Add(prductPhoto);
            }

            await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            return true;
        }


        public async Task<ProductUpdateVM> UpdateAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return null;

            var parentCategories = await _productCategoryRepository
                .GetAllAsync();




            var model = new ProductUpdateVM
            {
                Name = product.Name,
                CategoryId = product.CategoryId,
                Cost = product.Cost,
                Description = product.Description,
                MainPhotoPath = product.MainPhoto,
                TechnicalSpecifications = product.TechnicalSpecifications,
                IsMain = product.IsMain,
                ProductCategories = parentCategories,
                Photos = product.ProductPhots.ToList(),


            };


            return model;
        }


        public async Task<bool> UpdateAsync(ProductUpdateVM model, int id)
        {

            var existproduct = await _productRepository.GetProductsWithPhotos(id);

            if (existproduct is null) return false;
            existproduct.Name = model.Name;
            existproduct.DiscountPrice = model.DiscountPrice;
            existproduct.CategoryId = model.CategoryId;
            existproduct.Cost = model.Cost;
            existproduct.Description = model.Description;
            existproduct.IsMain = model.IsMain;
            existproduct.ProductPhots = model.Photos;



            existproduct.TechnicalSpecifications = model.TechnicalSpecifications;
            if (model.PhotoFiles != null)
            {

                foreach (var img in model.PhotoFiles)
                {
                    var prductPhoto = new ProductPhot
                    {
                        PhotoName = _fileService.Upload(img),
                        Product = existproduct
                    };

                    await _productPhotoRepository.CreateAsync(prductPhoto);

                }
            }


            await _unitOfWork.CommitAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                _modelState.AddModelError("Name", "Vision Goal doesn't exist");
                return false;
            }

            _productRepository.SoftDelete(product);
            foreach (var img in product.ProductPhots)
            {
                _fileService.Delete(img.PhotoName);

            }
            await _unitOfWork.CommitAsync();
            return true;
        }



    }

}
