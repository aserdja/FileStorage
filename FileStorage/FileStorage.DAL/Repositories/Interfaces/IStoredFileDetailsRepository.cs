using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IStoredFileDetailsRepository : IRepository<StoredFileDetails>
	{
        Task CreateAsync(StoredFileDetails storedFileDetails);

    }
}
