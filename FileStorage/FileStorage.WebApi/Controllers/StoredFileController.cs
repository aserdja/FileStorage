using FileStorage.BL.Models;
using FileStorage.BL.Services.FileServices.Interfaces;
using FileStorage.BL.Services.UserServices.Interfaces;
using FileStorage.DAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class StoredFileController(IFileService fileService, IUnitOfWork unitOfWork) : Controller
	{
		private readonly IFileService _fileService = fileService;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				var currentUserEmail = GetCurrentUserEmail();
				if (currentUserEmail == null)
				{
					return Unauthorized();
				}
				
				var currentUser = await _unitOfWork.Users.GetByEmailAsync(currentUserEmail);
				var fileUploading = CreateNewFileUploading(file);
				
				await _fileService.UploadFileAsync(fileUploading, currentUser);
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetFiles()
		{
			var currentUserEmail = GetCurrentUserEmail();
			if (currentUserEmail == null)
			{
				return Unauthorized();
			}

			try
			{
				var filesCollection = _fileService.GetFilesByEmailAsync(currentUserEmail);
				return Ok(await filesCollection);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		private FileUploading CreateNewFileUploading(IFormFile file)
		{
			return new FileUploading
			{
				FileName = file.FileName,
				ContentType = file.ContentType,
				Stream = file.OpenReadStream(),
				Length = file.Length,
				S3Path = $"{GetCurrentUserEmail()}/{file.FileName}"
			};
		}

		private string? GetCurrentUserEmail()
		{
			var claims = HttpContext.User.Claims;
			var currentUserEmail = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
			if (currentUserEmail != null)
			{
				return currentUserEmail;
			}
			return null;
		}
	}
}
