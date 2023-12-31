using Castle.DynamicProxy;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NLog.Web;
using shopapp.business.Concrete;
using shopapp.core.Business.Abstract;
using shopapp.core.CrossCuttingConcers.Caching;
using shopapp.core.CrossCuttingConcers.Caching.Microsoft;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using shopapp.core.Extensions;
using shopapp.core.Validation;
using shopapp.dataaccess.Concrete.EntityFramework;
using shopapp.web.EmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseNLog();

builder.Services.AddSingleton<ProxyGenerator>();
builder.Services.AddProxyScoped<IProductRepository, EfProductRepository>();
builder.Services.AddProxyScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddProxyScoped<ICartRepository, EfCartRepository>();
builder.Services.AddProxyScoped<ICartItemRepository, EfCartItemRepository>();
builder.Services.AddProxyScoped<IMainCategoryRepository, EfMainCategoryRepository>();
builder.Services.AddProxyScoped<IImageRepository, EfImageRepository>();
builder.Services.AddProxyScoped<ISubCategoryFeatureValueRepository, EfSubCategoryFeatureValueRepository>();
builder.Services.AddProxyScoped<ISubCategoryFeatureRepository, EfSubCategoryFeatureRepository>();
builder.Services.AddProxyScoped<ISubCategoryRepository, EfSubCategoryRepository>();
builder.Services.AddProxyScoped<IBrandRepository, EfBrandRepository>();
builder.Services.AddProxyScoped<IOrderRepository, EfOrderRepository>();

builder.Services.AddProxyScoped<IProductService, ProductService>();
builder.Services.AddProxyScoped<ICategoryService, CategoryService>();
builder.Services.AddProxyScoped<ICartService, CartService>();
builder.Services.AddProxyScoped<ICartItemService, CartItemService>();
builder.Services.AddProxyScoped<IMainCategoryService, MainCategoryService>();
builder.Services.AddProxyScoped<IImageService, ImageService>();
builder.Services.AddProxyScoped<ISubCategoryFeatureValueService, SubCategoryFeatureValueService>();
builder.Services.AddProxyScoped<ISubCategoryFeatureService, SubCategoryFeatureService>();
builder.Services.AddProxyScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddProxyScoped<IBrandService, BrandService>();
builder.Services.AddProxyScoped<IOrderService, OrderService>();

builder.Services.AddProxyScoped<ICacheManager, MemoryCacheManager>();


builder.Services.AddMemoryCache();


builder.Services.AddTransient<ShopContext>();


builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(
    i => new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:UserName"],
        builder.Configuration["EmailSender:Password"]
    ));

//using(var connection = new SqliteConnection("Data Source=shopdb.db;"))
//{
//    connection.Open();
//    connection.Close();
//}


using (ShopContext context = new ShopContext(builder.Configuration))
{
    var pendingMigrations = context.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
        context.Database.Migrate();
}

builder.Services.AddSession();
builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<ShopContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //options.User.AllowedUserNameCharacters = "";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/Login";
    options.LogoutPath = "/account/Logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});





// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = new PathString("/modules")
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "checkout",
    pattern: "checkout",
    defaults: new { controller = "Checkout", action = "Index" });

app.MapControllerRoute(
    name: "productcreate",
    pattern: "product/create",
    defaults: new { controller = "Product", action = "Create" });

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Admin", action = "Index" });
app.MapControllerRoute(
    name: "products",
    pattern: "products",
    defaults: new { controller = "Product", action = "Products" });
app.MapControllerRoute(
    name: "search",
    pattern: "search",
    defaults: new { controller = "Product", action = "Search" });
app.MapControllerRoute(
    name: "detail",
    pattern: "detail/{url}",
    defaults: new { controller = "Product", action = "Details" });

app.MapControllerRoute(
    name: "order",
    pattern: "order",
    defaults: new { controller = "Order", action = "Index" });

app.MapControllerRoute(
    name: "cart",
    pattern: "cart",
    defaults: new { controller = "Cart", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "",
    defaults: new { controller = "Product", action = "Products" });

app.MapControllerRoute(
    name: "product",
    pattern: "{category}",
    defaults: new { controller = "Product", action = "Index" });

app.MapControllerRoute(
    name: "product",
    pattern: "product",
    defaults: new { controller = "Product", action = "Index" });






app.MapControllerRoute(
    name: "cart",
    pattern: "cart",
    defaults: new { controller = "Cart", action = "Index" });

app.MapControllerRoute(
    name: "adminproductlist",
    pattern: "admin/product",
    defaults: new { controller = "Admin", action = "ProductList" });

app.MapControllerRoute(
    name: "adminroledelete",
    pattern: "admin/role/delete",
    defaults: new { controller = "Admin", action = "RoleDelete" });

app.MapControllerRoute(
    name: "adminroles",
    pattern: "admin/role",
    defaults: new { controller = "Admin", action = "RoleList" });

app.MapControllerRoute(
    name: "adminrolecreate",
    pattern: "admin/role/create",
    defaults: new { controller = "Admin", action = "RoleCreate" });

app.MapControllerRoute(
    name: "adminroleedit",
    pattern: "admin/role/{id?}",
    defaults: new { controller = "Admin", action = "RoleEdit" });

app.MapControllerRoute(
    name: "adminusers",
    pattern: "admin/user/list",
    defaults: new { controller = "Admin", action = "UserList" });

app.MapControllerRoute(
    name: "adminusercreate",
    pattern: "admin/user/create",
    defaults: new { controller = "Admin", action = "UserCreate" });

app.MapControllerRoute(
    name: "adminuseredit",
    pattern: "admin/user/{id?}",
    defaults: new { controller = "Admin", action = "UserEdit" });



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
    var user = await userManager.FindByIdAsync("8e445865-a24d-4543-a6c6-9443d048cdb9");
    if (user == null)
    {
        var hasher = new PasswordHasher<User>();
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var password = "";
        if (environment == "Development")
            password = builder.Configuration["Data:AdminUser:password"];

        if (environment == "Production")
            password = Environment.GetEnvironmentVariable("adminPassword");

        var adminUser = new User
        {
            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            FirstName = "Admin",
            LastName = "Admin",
            UserName = builder.Configuration["Data:AdminUser:username"],
            Email = builder.Configuration["Data:AdminUser:email"],
            NormalizedUserName = builder.Configuration["Data:AdminUser:username"].ToUpper(),
            NormalizedEmail = builder.Configuration["Data:AdminUser:email"].ToUpper(),
            PasswordHash = hasher.HashPassword(null, password),
            EmailConfirmed = true,
            LockoutEnabled = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };


        await userManager.CreateAsync(adminUser);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

}

app.Run();
