using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class WishlistProduct : BaseEntity
    {
        public int WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public bool IsInWishlist { get; set; }

    }
}
