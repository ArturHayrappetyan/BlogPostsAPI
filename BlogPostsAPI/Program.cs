
using BlogPostsAPI.Data;
using BlogPostsAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            // Register DbContext with PostgreSQL provider
            builder.Services.AddDbContext<BlogContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Swagger for API testing
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Middleware pipeline configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
