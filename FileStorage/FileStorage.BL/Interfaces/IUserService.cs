using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(string name, string login, string email, string password, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
       Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken);
        Task<User> GetByLoginAsync(string login);
    }
}