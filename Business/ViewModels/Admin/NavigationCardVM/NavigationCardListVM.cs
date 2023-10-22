using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.NavigationCardVM
{
    public class NavigationCardListVM
    {

        public NavigationCardListVM()
        {
            NavigationCard = new List<Common.Entities.NavigationCard>();
        }
        public List<Common.Entities.NavigationCard> NavigationCard { get; set; }
    }
}
