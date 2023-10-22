using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class NavigationCard: BaseEntity
    {
        public string Title { get; set; }

        public string BtnLink { get; set; }

        public string PhotoName { get; set; }
    }
}
