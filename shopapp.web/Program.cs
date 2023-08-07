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

builder.Services.AddProxyScoped<IProductService, ProductService>();
builder.Services.AddProxyScoped<ICategoryService, CategoryService>();
builder.Services.AddProxyScoped<ICartService, CartService>();
builder.Services.AddProxyScoped<ICartItemService, CartItemService>();

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
    name: "productcreate",
    pattern: "product/create",
    defaults: new { controller = "Product", action = "Create" });

app.MapControllerRoute(
    name: "product",
    pattern: "product",
    defaults: new { controller = "Product", action = "List" });


app.MapControllerRoute(
    name: "search",
    pattern: "search",
    defaults: new { controller = "Product", action = "Search" });

app.MapControllerRoute(
    name: "checkout",
    pattern: "checkout",
    defaults: new { controller = "Checkout", action = "Index" });

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

app.Run();
