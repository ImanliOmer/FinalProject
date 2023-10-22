using Common.Entities;
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
    public class NavigationCardRepository : Repository<NavigationCard>, INavigationCardRepository
    {
        private readonly AppDbContext _context;

        public NavigationCardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
