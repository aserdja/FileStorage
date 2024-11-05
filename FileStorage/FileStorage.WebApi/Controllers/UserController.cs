using FileStorage.BL.Models;
using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(IUserService userService) : Controller
	{
		private readonly IUserService _userService = userService;

		//dbcontext exception
		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromForm] UserRegistration newUser)
		{
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
			try
			{
				if (ModelState.IsValid)
				{
					if (await _userService.LogInUserAsync(user))
					{
						var claims = new List<Claim>()
						{
							new Claim(ClaimTypes.Name, user.Email),
							new Claim(ClaimTypes.Role, "User")
						};

						var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

						return Ok($"{claimsIdentity.Name} authenticated!");
					}
					return NotFound();
				}
				return BadRequest();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("signout")]
		public new async Task<IActionResult> SignOut()
		{
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
		public async Task<IActionResult> GetCurrentUser()
		{
			try
			{
				var claims = HttpContext.User.Claims;
				return Ok(claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
			}
			catch (Exception)
			{

				throw;
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
