using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
    public interface IUserRepository //: IRepository<User>
    {

        Task CreateAsync(User user, CancellationToken cancellationToken = default);

        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken);
        Task<User> GetByLoginAsync(string login);
    }
}
