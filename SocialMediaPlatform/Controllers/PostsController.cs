using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Dtos;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PostsController(AppDbContext context) => _context = context;

        //it can be done with way also
        /*        public PostsController(AppDbContext context)
                {
                    _context = context;
                }*/

        // this way also
        // public class PostsController(AppDbContext _context) : ControllerBase

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateDto dto)
        {
            var tags = await _context.Tags
                .Where(t => dto.TagIds.Contains(t.Id))
                .ToListAsync();

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                BlogId = dto.BlogId,
                Tags = tags
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(new PostReadDto(
                post.Id,
                post.Title,
                post.Content,
                post.Tags.Select(t => new TagReadDto(t.Id, t.Name)).ToList()
            ));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetPosts()
        {
            return await _context.Posts
                .Include(p => p.Tags)
                .Select(p => new PostReadDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.Tags.Select(t => new TagReadDto(t.Id, t.Name)).ToList()
                ))
                .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
