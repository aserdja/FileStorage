using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace FileStorage.DAL.Secrets
{
	public class DbSecretManager
	{
		private static string secretName = "dev/db/FileStorageDb";
		private static string region = "eu-central-1";

		public static async Task<string> GetDbCredentials()
		{
			try
			{
				IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
				GetSecretValueRequest request = new GetSecretValueRequest
				{
					SecretId = secretName,
				};

				var responce = await client.GetSecretValueAsync(request);
				return responce.SecretString;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
