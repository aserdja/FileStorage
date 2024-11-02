using FileStorage.BL.Models;
using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.UnitOfWork;

namespace FileStorage.BL.Services
{
	public class UserService(IUnitOfWork unitOfWork, IUserValidationService userValidationService) : IUserService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IUserValidationService userValidationService = userValidationService;

		public async Task<bool> RegisterUserAsync(string name, string login, string email, string password, string passwordConfirmation)
		{
			var newUser = new UserRegistration { Name = name, Login = login, Email = email, Password = password, PasswordConfirmation = passwordConfirmation };

			if (userValidationService.ValidateUserCredentials(newUser) != null)
			{
				_unitOfWork.Users.Add(ConvertToUser(newUser));
				
				await _unitOfWork.CommitAsync();
				return true;
			}

			return false;
		}

		private User ConvertToUser(UserRegistration userRegistrationModel)
		{
			return new User
			{
				Name = userRegistrationModel.Name,
				Login = userRegistrationModel.Login,
				Email = userRegistrationModel.Email,
				Password = userRegistrationModel.Password,
			};
		}
	}
}
