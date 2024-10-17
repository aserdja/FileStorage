using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.DAL.UnitOfWork
{
	public interface IUnitOfWork
	{
		IUserRepository Users { get; }
		IStoredFileRepository StoredFiles { get; }
		IStoredFileDetailsRepository StoredFilesDetails { get; }
		Task CommitAsync();
	}
}
