using BlogPostsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsAPI.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) 
            : base(options) { }

        public DbSet<Blog> Blogs { get; set; } 
        public DbSet<Post> Posts { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name)
                .IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title)
                .IsRequired();
                entity.Property(p => p.Content)
                .IsRequired();

                entity.HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
