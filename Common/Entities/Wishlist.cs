using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Wishlist : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public ICollection<WishlistProduct>? WishlistProducts { get; set; }
    }
}
