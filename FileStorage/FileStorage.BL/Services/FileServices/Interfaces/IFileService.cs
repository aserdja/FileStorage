using FileStorage.BL.Models;
using FileStorage.DAL.Models;

namespace FileStorage.BL.Services.FileServices.Interfaces
{
	public interface IFileService
	{
		Task<StoredFile?> UploadFileAsync(FileUploading fileToUpload, User currentUser);
		Task<bool> DownloadFileAsync(string fileName, string currentUserEmail);
		Task<ICollection<StoredFile>> GetFilesByEmailAsync(string currentUserEmail);
	}
}
