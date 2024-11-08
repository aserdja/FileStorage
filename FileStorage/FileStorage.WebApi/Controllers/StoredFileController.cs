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

		[HttpPost("upload")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			var currentUserEmail = GetCurrentUserEmail();
			if (currentUserEmail == null)
			{
				return Unauthorized();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				
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

		[HttpGet("getall")]
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

		[HttpPost("download")]
		public async Task<IActionResult> DownloadFile([FromForm] int fileId)
		{
			var currentUserEmail = GetCurrentUserEmail();
			if (currentUserEmail == null)
			{
				return Unauthorized();
			}

			try
			{
				var fileToDownload = await _unitOfWork.StoredFiles.GetByIdAsync(fileId);
				var fileOwner = await _unitOfWork.Users.GetByEmailAsync(currentUserEmail);

				if (fileToDownload == null)
				{
					return NotFound("There is no file with this ID!");
				}

				if (fileToDownload.UserId != fileOwner?.Id)
				{
					return Forbid("This file is not available for you to download!");
				}

				bool isDownloaded = await _fileService.DownloadFileAsync(fileToDownload.Name, currentUserEmail);

				if (!isDownloaded)
				{
					return StatusCode(500);
				}

				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteFile([FromForm] int fileId)
		{
			var currentUserEmail = GetCurrentUserEmail();
			if (currentUserEmail == null)
			{
				return Unauthorized();
			}

			try
			{
				var fileToDelete = await _unitOfWork.StoredFiles.GetByIdAsync(fileId);
				var fileDetailsToDelete = await _unitOfWork.StoredFilesDetails.GetByStoredFileId(fileToDelete.Id);
				var fileOwner = await _unitOfWork.Users.GetByEmailAsync(currentUserEmail);

				if (fileToDelete == null)
				{
					return NotFound("There is no file with this ID!");
				}

				if (fileToDelete.UserId != fileOwner?.Id)
				{
					return Forbid("This file is not available for you to delete!");
				}

				bool isDeleted = await _fileService.DeleteFileAsync(fileToDelete.Name, currentUserEmail);
				if (!isDeleted)
				{
					return StatusCode(500);
				}

				_unitOfWork.StoredFilesDetails.Delete(fileDetailsToDelete);
				_unitOfWork.StoredFiles.Delete(fileToDelete);
				await _unitOfWork.CommitAsync();

				return Ok();
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
