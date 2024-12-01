using BlogPostsAPI.Models;

namespace BlogPostsAPI.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task<Blog> AddBlogAsync(Blog blog);
    }
}
