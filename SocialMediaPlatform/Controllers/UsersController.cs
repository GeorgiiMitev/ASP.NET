using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services;

namespace SocialMediaPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;    
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            return Ok(_userService.Create(user));
        }

        [HttpPut]
        public IActionResult Update([FromQuery] int id, [FromBody] User user)
        {

            return Ok(_userService.UpdateUsers(id, user));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
           
           
                return Ok(_userService.Delete(id));
            
            

        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            User user = _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest($"User was not found");

        }
    }
}
