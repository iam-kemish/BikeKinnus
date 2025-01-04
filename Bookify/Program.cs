using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.DataAccess.RepositaryClasses;
using BikeKinnus.Database;
using BikeKinnus.Repositary;
using BikeKinnus.RepositaryClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



////////////
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
// This was changed to

//this

//Identity initialization
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//options => options.SignIn.RequireConfirmedAccount = true, was removed so that user doesnt have to confirm his/her email while logging in.
////////////
///
//Cookie identity routing configuration always after Identity is initialized.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});



builder.Services.AddScoped<ICategory,CategoryClass>();
builder.Services.AddScoped<IProduct, ProductClass>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ICompany, CompanyClass>();
builder.Services.AddScoped<IBuyingCart, BuyingCartClass>();
builder.Services.AddScoped<IAppUser, AppUserClass>();
builder.Services.AddScoped<IOrderHeader, OrderHeaderClass>();
builder.Services.AddScoped<IOrderDetail, OrderDetailsClass>();
builder.Services.AddRazorPages();
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

//Authentication was added before Authorization because before giving user specific tasks on the website according to their roles, we need to sign in a valid user.
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
