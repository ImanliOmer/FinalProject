using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class NavigationCardRepository : Repository<NavigationCard>, INavigationCardRepository
    {
        private readonly AppDbContext _context;

        public NavigationCardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<NavigationCard> GetByNameAsync(string name)
		{
			return await _context.NavigationCards.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Title.ToLower().Trim() == name.ToLower().Trim());
		}
	}
}
