using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.NavigationCardVM
{
    public class NavigationCardUpdateVM
    {
        public string Title { get; set; }

        public string BtnLink { get; set; }

        public string? Image { get; set; }

        public IFormFile? NewImage { get; set; }
    }
}
