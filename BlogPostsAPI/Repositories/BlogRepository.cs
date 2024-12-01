using BlogPostsAPI.Data;
using BlogPostsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsAPI.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.Posts).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.Posts)
                                       .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Blog> AddBlogAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
