﻿using FileStorage.BL.Models;
using FileStorage.BL.Services.FileServices.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.UnitOfWork;

namespace FileStorage.BL.Services.FileServices
{
	public class FileService(IUnitOfWork unitOfWork, IS3BucketService bucketService) : IFileService
	{
		private readonly IS3BucketService _bucketService = bucketService;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<StoredFile?> UploadFileAsync(FileUploading fileToUpload, User currentUser)
		{
			bool uploadingResult = await _bucketService.UploadFileToS3BucketAsync(fileToUpload, currentUser.Email);
			if (!uploadingResult)
			{
				throw new Exception();
			}

			var storedFile = ConvertToStoredFile(fileToUpload, currentUser);
			_unitOfWork.StoredFiles.Add(storedFile);
			await _unitOfWork.CommitAsync();

			return storedFile;
		}

		private StoredFile ConvertToStoredFile(FileUploading fileToUpload, User currentUser)
		{
			return new StoredFile
			{
				Name = fileToUpload.FileName,
				Type = fileToUpload.ContentType,
				Size = fileToUpload.Length / Math.Pow(1024, 3),
				Path = $"{currentUser.Email}/{fileToUpload.FileName}",
				User = currentUser
			};
		}
	}
}