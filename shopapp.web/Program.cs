using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;
using shopapp.web.EmailService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient(typeof(IProductRepository), typeof(EfProductRepository));
builder.Services.AddTransient(typeof(ICategoryRepository), typeof(EfCategoryRepository));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddTransient(typeof(ShopContext), typeof(ShopContext));

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(
    i =>new SmtpEmailSender(
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

builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<ShopContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit=true;
    options.Password.RequireLowercase=true;
    options.Password.RequireUppercase=true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric=true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //options.User.AllowedUserNameCharacters = "";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail=true;
    options.SignIn.RequireConfirmedPhoneNumber=false;

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
        Name=".ShopApp.Security.Cookie",
        SameSite=SameSiteMode.Strict
    };
});


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
