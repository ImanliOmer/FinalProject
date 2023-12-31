﻿using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class ProductPhotoRepository: Repository<ProductPhot> , IProductPhotoRepository
	{
		private readonly AppDbContext _context;

		public ProductPhotoRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}


	}
}
