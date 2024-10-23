using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL;
using Microsoft.EntityFrameworkCore;
namespace FileStorage.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FileStorageDbContext _dbContext;

        public UserRepository(FileStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Login == login, cancellationToken);
        }
        public async Task<User> GetByLoginAsync(string login)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Login == login);
        }
    }

}
