using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Wishlist
{
    public class WishlistVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public string PhotoName { get; set; }
        public int ProductId { get; set; }
        //public bool IsInWishlist { get; set; }
    }
}
