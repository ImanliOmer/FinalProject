using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.NavigationCardVM;
using Business.ViewModels.Admin.Product;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.Services.Concret.Admin
{
    public class NavigationCardService : INavigationCardService
    {

        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INavigationCardRepository _navigationCardRepository;
        private readonly Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary _modelState;


        public NavigationCardService(IFileService fileService,
            IActionContextAccessor contextAccessor,
                                    IUnitOfWork unitOfWork,
                                   INavigationCardRepository navigationCardRepository)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _navigationCardRepository = navigationCardRepository;
            _modelState = contextAccessor.ActionContext.ModelState;

        }


        public async Task<bool> CreateAsync(NavigationCardCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Image))
            {
                _modelState.AddModelError("Photo", "Wrong file format");
                return false;
            }

			var nvc = await _navigationCardRepository.GetByNameAsync(model.Title);
			if (nvc is not null)
			{
				_modelState.AddModelError("Title", "Title under this name already exists in database");
				return false;
			}

			if (_fileService.IsBiggerThanSize(model.Image, 200))
            {
                _modelState.AddModelError("Photo", "File size is over 200kb");
                return false;
            }

            var navCard = new NavigationCard
            {
                Title = model.Title,
                BtnLink = model.BtnLink,
                PhotoName = _fileService.Upload(model.Image),
                CreatedAt = DateTime.Now,
            };

            await _navigationCardRepository.CreateAsync(navCard);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var navcard = await _navigationCardRepository.GetByIdAsync(id);
            if (navcard is null)
            {
                _modelState.AddModelError("Title", "Title doesn't exist");
                return false;
            }

            _navigationCardRepository.SoftDelete(navcard);
            _fileService.Delete(navcard.PhotoName);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<NavigationCardListVM> GetAllAsync()
        {
            var model = new NavigationCardListVM
            {
                NavigationCard = await _navigationCardRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<NavigationCardUpdateVM> UpdateAsync(int id)
        {
            var navcard = await _navigationCardRepository.GetByIdAsync(id);
            if (navcard is null) return null;

            var model = new NavigationCardUpdateVM
            {
                Title = navcard.Title,
                Image = navcard.PhotoName,
                BtnLink = navcard.BtnLink,
            };

            return model;
        }

        public async Task<bool> UpdateAsync(NavigationCardUpdateVM model, int id)
        {
            if (!_modelState.IsValid) return false;


            var navcard = await _navigationCardRepository.GetByIdAsync(id);
            if (navcard is null) return false;

           
            if (navcard is null)
            {
                _modelState.AddModelError("Title", "Title doesn't exist");
                return false;
            }

            navcard.Title = model.Title;
            navcard.BtnLink = model.BtnLink;
            

            if (model.NewImage is not null)
            {

                if (!_fileService.IsImage(model.NewImage))
                {
                    _modelState.AddModelError("Image", "Wrong file format");
                    return false;
                }

                if (_fileService.IsBiggerThanSize(model.NewImage, 200))
                {
                    _modelState.AddModelError("Image", "File size is over 200kb");
                    return false;
                }

                _fileService.Delete(navcard.PhotoName);
                navcard.PhotoName = _fileService.Upload(model.NewImage);
            };
            navcard.ModfiedAt = DateTime.Now;

            _navigationCardRepository.Update(navcard);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
