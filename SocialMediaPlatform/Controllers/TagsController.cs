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
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TagsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagReadDto>>> GetTags()
        {
            return await _context.Tags
                .Select(t => new TagReadDto(t.Id, t.Name))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TagReadDto>> CreateTag(TagCreateDto dto)
        {
            var tag = new Tag { Name = dto.Name };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return Ok(new TagReadDto(tag.Id, tag.Name));
        }
    }
}
