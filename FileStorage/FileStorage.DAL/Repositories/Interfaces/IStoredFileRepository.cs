﻿using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IStoredFileRepository : IRepository<StoredFile>
	{
		Task<ICollection<StoredFile>> GetAllByEmailAsync(string userEmail);
		Task<StoredFile?> GetByIdAsync(int id);
	}
}
