using FileStorage.BL.Models;

namespace FileStorage.BL.Services.FileServices.Interfaces
{
	public interface IS3BucketService
	{
		Task<bool> UploadFileToS3BucketAsync(FileUploading fileUploading, string currentUserEmail);
	}
}
