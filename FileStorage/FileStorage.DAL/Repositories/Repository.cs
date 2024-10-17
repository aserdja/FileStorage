using FileStorage.DAL.Data;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly FileStorageDbContext _context;
		private readonly DbSet<T> _dbSet;

		public Repository(FileStorageDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}
	}
}
