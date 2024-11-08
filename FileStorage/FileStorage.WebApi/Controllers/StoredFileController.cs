using AutoMapper;
using FileStorage.BL.Models;
using FileStorage.BL.Services.FileServices.Interfaces;
using FileStorage.BL.Services.UserServices.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.UnitOfWork;
using FileStorage.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class StoredFileController(IFileService fileService, IUnitOfWork unitOfWork, IMapper mapper) : Controller
	{
		private readonly IFileService _fileService = fileService;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IMapper _mapper = mapper;

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
				var storedFilesCollection = _fileService.GetFilesByEmailAsync(currentUserEmail).Result.ToList();
				var storedFilesDetailsCollection = _unitOfWork.StoredFilesDetails.GetAllByStoredFilesAsync(storedFilesCollection).Result.ToList();
				List<StoredFileDetailsDTO> result = new();
				
				if (storedFilesDetailsCollection == null)
				{
					Ok("You haven't uploaded files to storage yet");
				}

				for (int i = 0; i < storedFilesCollection.Count; i++)
				{
					var storedFileDetailsDTO = _mapper.Map<StoredFileDetailsDTO>(storedFilesCollection[i]);
					var mappedModel = _mapper.Map(storedFilesDetailsCollection[i], storedFileDetailsDTO);
					result.Add(mappedModel);
				}
				
				return Ok(result);
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
