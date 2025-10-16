
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;

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

            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Generic Servis

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); // cors hatasýna takýlan tüm istekleri kabul et
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("default");

            app.MapControllers();

            app.Run();
        }
    }
}
