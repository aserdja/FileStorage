using FileStorage.BL.Interfaces;
using FileStorage.BL.Services;
using FileStorage.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create user")]
        public async Task<IActionResult> CreateAsync(
     string name,
     string login,
     string email,
     string password)
        {
            await _userService.CreateAsync(name, login, email, password);
            return (IActionResult)this.NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(id, cancellationToken);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("login/{login}")]
        public async Task<IActionResult> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByLoginAsync(login, cancellationToken);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
