using Microsoft.AspNetCore.Authentication.JwtBearer; // jwt token g�venlik k�t�phanesi
using Microsoft.IdentityModel.Tokens;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;
using System.Text;

namespace SH1ProjeUygulamasi.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    // validasyon yapmak istedi�imiz alanlar:
                    ValidateAudience = true, // kitleyi do�rula
                    ValidateIssuer = true, // token vereni do�rula
                    ValidateLifetime = true, // token ya�am s�resini do�rula
                    ValidateIssuerSigningKey = true, // token verenin imzalama anahtar�n� do�rula
                    ValidIssuer = builder.Configuration["Token:Issuer"], // token veren sa�lay�c�y� appsettings.json dan �ek
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero // saat fark� olmas�n
                };
            });

            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Generic Servis

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); // cors hatas�na tak�lan t�m istekleri kabul et
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // �nce oturum a�ma
            app.UseAuthorization(); // sonra yetkilendirme

            app.UseStaticFiles(); // api de statik dosyalar� kullanmak i�in

            app.UseCors("default");

            app.MapControllers();

            app.Run();
        }
    }
}
