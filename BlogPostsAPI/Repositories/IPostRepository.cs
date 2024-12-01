using BlogPostsAPI.Models;

namespace BlogPostsAPI.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> AddPostAsync(Post post);
    }
}
