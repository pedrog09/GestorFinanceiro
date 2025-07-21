using GestorFinanceiro.Adapter;
using GestorFinanceiro.Services;
using Microsoft.OpenApi.Models; // Adicione esta diretiva de namespace para resolver o erro.
using Swashbuckle.AspNetCore.SwaggerGen; // Adicione esta diretiva de namespace para habilitar o método de extensão AddSwaggerGen.


namespace GestorFinanceiro 
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Admin.API",
                    Description = "API de Admin"
                });
            });

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserAdapter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Corrige o método para habilitar Swagger.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestorFinanceiro API v1")); // Adiciona a interface do Swagger.
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
