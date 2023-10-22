using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Basket : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
