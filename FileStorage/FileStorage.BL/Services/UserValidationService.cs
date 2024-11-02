using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace FileStorage.BL.Services
{
	public class UserValidationService(IUnitOfWork unitOfWork) : IUserValidationService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<bool> CheckCredentialsUniqueness(string login, string email)
		{
			if (await _unitOfWork.Users.GetByLoginAsync(login) != null)
			{
				return false;
			}
			if (await _unitOfWork.Users.GetByEmailAsync(email) != null)
			{
				return false;
			}

			return true;
		}

		public bool CheckCredentialsLength(string name, string login, string email, string password)
		{
			if (name.Trim().IsNullOrEmpty() && name.Length > 20)
			{
				return false;
			}
			if (login.Trim().IsNullOrEmpty() && login.Length > 20)
			{
				return false;
			}
			if (email.Trim().IsNullOrEmpty() && email.Length > 64)
			{
				return false;
			}
			if (password.Trim().IsNullOrEmpty() && password.Length > 24)
			{
				return false;
			}

			return true;
		}
	}
}
