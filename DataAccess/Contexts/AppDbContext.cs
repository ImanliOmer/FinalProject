using Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
	public class AppDbContext: IdentityDbContext<IdentityUser>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Slider> Sliders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<NavigationCard> NavigationCards { get; set; }
        public DbSet<Wishlist> Wishlists { get; set;}
        public DbSet<WishlistProduct> WishlistProducts { get; set;}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductCategory>()
        //        .HasMany(pc => pc.Children) 
        //        .WithOne(child => child.ParentCategory) 
        //        .HasForeignKey(child => child.ParentId);

        //    modelBuilder.Entity<ProductCategory>()
        //       .HasMany(pc => pc.Products)
        //       .WithOne(child => child.Category)
        //       .HasForeignKey(child => child.CategoryId);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
