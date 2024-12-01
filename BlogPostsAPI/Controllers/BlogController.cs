using BlogPostsAPI.Data;
using BlogPostsAPI.DTOs;
using BlogPostsAPI.Models;
using BlogPostsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            var blogs = await _blogRepository.GetAllBlogsAsync();
            var blogDTOs = blogs.Select(b => new BlogDTO
            {
                BlogId = b.Id,
                Name = b.Name
            }).ToList();

            return blogDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);

            if (blog == null)
                return NotFound();

            var blogDTO = new BlogDTO
            {
                BlogId = blog.Id,
                Name = blog.Name
            };

            return blogDTO;
        }

        [HttpPost]
        public async Task<ActionResult<BlogDTO>> Create(BlogDTO blogDTO)
        {
            var blog = new Blog { Name = blogDTO.Name };

            var createdBlog = await _blogRepository.AddBlogAsync(blog);
            return CreatedAtAction(nameof(GetBlogs), new { id = createdBlog.Id }, blogDTO);
        }
    }
}
