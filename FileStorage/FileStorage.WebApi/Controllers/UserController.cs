using FileStorage.BL.Models;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FileStorage.BL.Services.UserServices.Interfaces;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController(IUserService userService) : Controller
	{
		private readonly IUserService _userService = userService;

		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromForm] UserRegistration newUser)
		{
			if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
			{
				return Conflict("You are already logged in!");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				bool registrationResult = await _userService.RegisterUserAsync(newUser);
				if (!registrationResult)
				{
					return BadRequest();
				}

				bool loginResult = await _userService.LogInUserAsync(new UserAuthentication { Email = newUser.Email, Password = newUser.Password });
				if (!loginResult)
				{
					return Unauthorized();
				}

				await AuthenticateCookieSession(newUser.Email);
				return Ok($"{newUser.Email} authenticated!");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("signin")]
		public async Task<IActionResult> SingIn([FromForm] UserAuthentication user)
		{
			if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
			{
				return Conflict("You are already logged in!");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				bool loginResult = await _userService.LogInUserAsync(user);
				if (!loginResult)
				{
					return Unauthorized();
				}

				await AuthenticateCookieSession(user.Email);
				return Ok($"{user.Email} authenticated!");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("signout")]
		public new async Task<IActionResult> SignOut()
		{
			if (User != null && User.Identity != null && !User.Identity.IsAuthenticated)
			{
				return Conflict("You are not authorized to your account!");
			}

			var claims = HttpContext.User.Claims;

			if (claims.FirstOrDefault(x => x.Type == ClaimTypes.Name) != null)
			{
				await HttpContext.SignOutAsync();
				return Ok();
			}

			return StatusCode(500);
		}

		//test-method
		[HttpGet]
		public IActionResult GetCurrentUser()
		{
			try
			{
				var claims = HttpContext.User.Claims;

				if (claims.FirstOrDefault(x => x.Type == ClaimTypes.Name) == null)
				{
					return Unauthorized();
				}

				return Ok(claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		private async Task AuthenticateCookieSession(string userEmail)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, userEmail),
				new Claim(ClaimTypes.Role, "User")
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
		}
	}
}
