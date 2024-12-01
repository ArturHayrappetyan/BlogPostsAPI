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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost(PostDTO postDTO)
        {
            var post = new Post
            {
                Title = postDTO.Title,
                Content = postDTO.Content,
                BlogId = postDTO.BlogId,
            };

            var createdPost = await _postRepository.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPost),new {id = createdPost.Id},postDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }

            var postDTO = new PostDTO
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId,
            };

            return postDTO;
        }
    }
}
