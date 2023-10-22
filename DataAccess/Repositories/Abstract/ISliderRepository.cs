using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface ISliderRepository : IRepository<Slider>
	{
		public Slider GetSlider();
		Task<List<Slider>> GetSliderWithPhotos();
	}
}
