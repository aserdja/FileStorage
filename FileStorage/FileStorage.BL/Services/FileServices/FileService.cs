using FileStorage.BL.Models;
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
			var storedFileDetails = CreateStoredFileDetails(storedFile);

			_unitOfWork.StoredFiles.Add(storedFile);
			_unitOfWork.StoredFilesDetails.Add(storedFileDetails);

			await _unitOfWork.CommitAsync();

			return storedFile;
		}

		public async Task<bool> DownloadFileAsync(string fileName, string currentUserEmail)
		{
			bool downloadingResult = await _bucketService.DownloadFileFromS3BucketAsync(fileName, currentUserEmail);
			if (!downloadingResult)
			{
				return false;
			}
			return true;			
		}

		public async Task<bool> DeleteFileAsync(string fileName, string currentUserEmail)
		{
			bool deleteResult = await _bucketService.DeleteFileFromS3BucketAsync(fileName, currentUserEmail);
			if (!deleteResult)
			{
				return false;
			}
			return true;
		}

		public async Task<ICollection<StoredFile>> GetFilesByEmailAsync(string currentUserEmail)
		{
			return await _unitOfWork.StoredFiles.GetAllByEmailAsync(currentUserEmail);
		}

		private StoredFile ConvertToStoredFile(FileUploading fileToUpload, User currentUser)
		{
			return new StoredFile
			{
				Name = fileToUpload.FileName,
				Type = fileToUpload.ContentType,
				Size = 0.0,
				Path = $"{currentUser.Email}/{fileToUpload.FileName}",
				User = currentUser
			};
		}

		private StoredFileDetails CreateStoredFileDetails(StoredFile storedFile)
		{
			return new StoredFileDetails
			{
				UploadDateTime = DateTime.Now,
				ExpireDateTime = DateTime.Now.AddMonths(2),
				StoredFile = storedFile
			};
		}
	}
}
