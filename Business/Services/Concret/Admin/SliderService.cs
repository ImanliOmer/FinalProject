using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using Business.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concret.Admin
{
	public class SliderService : ISliderService
	{
		private readonly ISliderRepository _sliderRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary _modelState;

		public SliderService(ISliderRepository sliderRepository,
							  IActionContextAccessor contextAccessor,
							  IFileService fileService,
							  IUnitOfWork unitOfWork)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_sliderRepository = sliderRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> CreateAsync(SliderCreateVM model)
		{
			if (!_modelState.IsValid) return false;

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

			var slider = new Slider
			{
				Title = model.Title,
				Description = model.Description,
				Photo = _fileService.Upload(model.Photo),
				CreatedAt = DateTime.Now,
			};

			await _sliderRepository.CreateAsync(slider);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			_sliderRepository.SoftDelete(slider);
			_fileService.Delete(slider.Photo);
			await _unitOfWork.CommitAsync();
			return true;
		}
			
		public async Task<List<SliderListItemVM>> GelAllASync()
		{
			var dbSliders = await _sliderRepository.GetAllAsync();

			var model = new List<SliderListItemVM>();
			foreach (var dbSlider in dbSliders)
			{
				model.Add(new SliderListItemVM
				{
					Id = dbSlider.Id,
					Title = dbSlider.Title,
					Photo = dbSlider.Photo,
					CreatedAt = dbSlider.CreatedAt,
					ModifiedAt = dbSlider.ModfiedAt,
				});
			}
			return model;
		}

		public async Task<SliderUpdateVM> UpdateAsync(int id)
		{
			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null) return null;

			var model = new SliderUpdateVM
			{
				Title = slider.Title,
				Description = slider.Description,
				Photo = slider.Photo,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(SliderUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			slider.Title = model.Title;
			slider.Description = model.Description;
			slider.ModfiedAt = DateTime.Now;
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

				if (!string.IsNullOrEmpty(slider.Photo))
				{
					_fileService.Delete(slider.Photo);
				}
				slider.Photo = _fileService.Upload(model.NewPhoto);
			}
			_sliderRepository.Update(slider);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
