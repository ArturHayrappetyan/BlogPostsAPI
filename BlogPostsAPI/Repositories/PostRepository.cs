using BlogPostsAPI.Data;
using BlogPostsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }


        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.Blog)
                                       .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
