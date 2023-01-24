global using HamburgerShop.Models.Context;
global using Microsoft.EntityFrameworkCore;
global using HamburgerShop.Models.Data.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Hamburger>();
builder.Services.AddScoped<Siparis>();

string baglantiCumlem = builder.Configuration.GetConnectionString("ApplicationDbContext");
builder.Services.AddDbContext<UygulamaDbContext>(o => o.UseSqlServer(baglantiCumlem));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
