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
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CommentsController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                PostId = dto.PostId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new CommentReadDto(comment.Id, comment.Content, comment.CreatedAt));
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentReadDto>>> GetByPost(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Select(c => new CommentReadDto(c.Id, c.Content, c.CreatedAt))
                .ToListAsync();
        }
    }
}
