using Business.ViewModels.Admin.NavigationCardVM;
using Business.ViewModels.Admin.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface INavigationCardService
    {
        Task<NavigationCardListVM> GetAllAsync();
        Task<bool> CreateAsync(NavigationCardCreateVM model);
        Task<bool> DeleteAsync(int id);
        Task<NavigationCardUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(NavigationCardUpdateVM model, int id);

    }
}
