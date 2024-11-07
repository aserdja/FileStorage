using FileStorage.BL.Models;
using FileStorage.DAL.Models;

namespace FileStorage.BL.Services.FileServices.Interfaces
{
	public interface IFileService
	{
		Task<StoredFile?> UploadFileAsync(FileUploading fileToUpload, User currentUser);
	}
}
