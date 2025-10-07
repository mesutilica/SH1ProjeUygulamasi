using Microsoft.AspNetCore.Authentication.Cookies;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;
using System.Security.Claims;

namespace SH1ProjeUygulamasi.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddScoped<ICategoryService, CategoryService>(); // Uygulamaya ICategoryService i kullanma isteði gelirse CategoryService sýnýfýndan bir nesne oluþtur ve onu kullan.

            // builder.Services.AddSingleton<ICategoryService, CategoryService>();
            // builder.Services.AddTransient<ICategoryService, CategoryService>();

            builder.Services.AddTransient<IProductService, ProductService>(); // Servisi buraya tanýtmazsak hata alýrýz!!!

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Generic Servis

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            // Authorization : Yetkilendirme : once servis olarak ekliyoruz
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")); // Bundan sonra Controller lara Policy i belirtmeliyiz..
                x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
