using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IStoredFileDetailsRepository : IRepository<StoredFileDetails>
	{
		Task<ICollection<StoredFileDetails?>> GetAllByStoredFilesAsync(ICollection<StoredFile> storedFiles);
	}
}
