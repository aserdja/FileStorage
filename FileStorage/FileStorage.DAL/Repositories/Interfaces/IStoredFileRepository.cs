using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IStoredFileRepository : IRepository<StoredFile>
	{
        Task CreateAsync(StoredFile storedFile);

    }
}
