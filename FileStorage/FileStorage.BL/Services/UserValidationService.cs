using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.UnitOfWork;

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
	}
}
