using FileStorage.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.Models;

namespace FileStorage.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(string name, string login, string email, string password, CancellationToken cancellationToken = default)
        {
            var user = new User { Name = name, Login = login, Email = email, Password = password };
            await _userRepository.CreateAsync(user, cancellationToken);
        }


        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByLoginAsync(login, cancellationToken);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _userRepository.GetByLoginAsync(login);
        }
    }
}
