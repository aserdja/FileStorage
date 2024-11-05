using FileStorage.BL.Models;
using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.UnitOfWork;
using System.Net.Http;
using System.Security.Claims;

namespace FileStorage.BL.Services
{
	public class UserService(IUnitOfWork unitOfWork, IUserValidationService userValidationService) : IUserService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IUserValidationService userValidationService = userValidationService;

		public async Task<bool> RegisterUserAsync(UserRegistration newUser)
		{
			var validationResult = await userValidationService.ValidateUserCredentials(newUser);
			
			if (validationResult != null)
			{
				_unitOfWork.Users.Add(ConvertToUser(newUser));
				await _unitOfWork.CommitAsync();

				return true;
			}

			return false;
		}

		public async Task<bool> LogInUserAsync(UserAuthentication userAuthentication)
		{
			var user = await _unitOfWork.Users.GetByEmailAndPasswordAsync(userAuthentication.Email, userAuthentication.Password);

			if (user != null)
			{
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
