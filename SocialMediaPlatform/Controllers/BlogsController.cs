using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Dtos;
using SocialMediaPlatform.Models;
using System.Linq;

namespace SocialMediaPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController(AppDbContext _context) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogCreateDto dto)
        {
            // Check if there is a user
            var user = await _context.Users.Include(u => u.Blog).FirstOrDefaultAsync(u => u.Id == dto.UserId);
            if (user == null) return NotFound("User not found");

            // Enforce 1:1 constains, if user has a blog an error is shown
            if (user.Blog != null) return BadRequest("This user already has a blog.");

            var blog = new Blog
            {
                Name = dto.Name,
                UserId = dto.UserId
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(new BlogReadDto(blog.Id, blog.Name, blog.UserId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogReadDto>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return new BlogReadDto(blog.Id, blog.Name, blog.UserId);
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogReadDto>>> GetAllBlog(string? Name = null, string? Username = null, string OrderBy = "DESC",int pageNumber = 1, int pageSize = 10)
        {
            

            var blogs = await _context.Blogs.Include(b => b.User).ToListAsync();
            if (Name != null)
            {
                blogs = blogs.Where(p => p.Name.Contains(Name)).ToList();
            }
            if (Username != null)
            {
                blogs = blogs.Where(p => p.User.Username.Contains(Username)).ToList();
            }
            


            var blogDtos = blogs.Select(b => new BlogReadDtoList(
                b.Id,
                b.Name,
                new UserReadDto(b.User.Id, b.User.Username, b.Name),
                b.Posts.Select(p => new PostReadDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.Tags.Select(t => new TagReadDto(t.Id, t.Name)).ToList()
                )).ToList()
            )).ToList();

            if (OrderBy.ToUpper() == "ASC")
            {
                blogDtos = blogDtos.OrderBy(b => b.Id).ToList();
            }

            blogDtos = blogDtos.OrderByDescending(b => b.Id).ToList();

            var pagedData = blogDtos.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            if (blogDtos == null) return NotFound();
            return Ok(blogDtos);
        }
    }
}
