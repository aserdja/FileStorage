using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.Tests
{
	public class UserRepositoryIntegrationTests
	{
		[Fact]
		public async Task GetByEmailAsync_expects_get_user()
		{
			var context = new FileStorageDbContext();
			IUserRepository userRepository = new UserRepository(context);
			var email = "User1@user.test";
			var user = new User { Name = "User1", Email = email, Login = "Test_user_1", Password = "123123asd" };
			context.Add(user);
			await context.SaveChangesAsync();


			var result = await userRepository.GetByEmailAsync(email);


			Assert.Equal(user, result);

			context.Remove(user);
			await context.SaveChangesAsync();
		}

		[Fact]
		public async Task GetByLoginAsync_expects_get_user()
		{
			var context = new FileStorageDbContext();
			IUserRepository userRepository = new UserRepository(context);
			var login = "login@test";
			var user = new User { Name = "User1", Email = "test@test.test", Login = login, Password = "123123asd" };
			context.Add(user);
			await context.SaveChangesAsync();


			var result = await userRepository.GetByLoginAsync(login);


			Assert.Equal(user, result);

			context.Remove(user);
			await context.SaveChangesAsync();
		}
	}
}
