using Business.Services.Abstract.Admin;
using Business.Services.Abstract.User;
using Business.Services.Concret.Admin;
using Business.Services.Concret.User;
using Business.Services.Concret.Users;
using Business.Utilities.EmailService;
using Business.Utilities.EmailService.EmailSender.Abstract;
using Business.Utilities.EmailService.EmailSender.Concret;
using Business.Utilities.File;
using Common.Entities;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;

#region builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var configuration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.HttpContext.Request.Path.Value.StartsWith("/admin") || context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/admin/account/login" + redirectPath.Query);
        }
        else
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/account/login" + redirectPath.Query);
        }
        return Task.CompletedTask;
    };
});
#endregion

//service start
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<INavigationCardService, NavigationCardService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

builder.Services.AddScoped<IBasketService, BasketService>();


//service end

//////////////*****************//////////////////

//repositroy start
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
builder.Services.AddScoped<INavigationCardRepository, NavigationCardRepository>();    
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IWishlistProductRepository, WishlistProductRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
//repository end



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
    await DbInitializer.SeedAsync(roleManager, userManager);
}

app.MapControllerRoute(
		  name: "areas",
		  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		);
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();