using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using FileStorage.BL.Models;
using FileStorage.BL.Services.FileServices.Interfaces;

namespace FileStorage.BL.Services.FileServices
{
	public class S3BucketService: IS3BucketService
	{
		private readonly IAmazonS3 _s3Client = new AmazonS3Client(RegionEndpoint.EUCentral1);
		private readonly string _bucketName = "filestorage.s3bucket";

		public async Task<bool> UploadFileToS3BucketAsync(FileUploading fileUploading, string currentUserEmail)
		{
			var request = new PutObjectRequest()
			{
				BucketName = _bucketName,
				Key = $"{currentUserEmail}/{fileUploading.FileName}",
				InputStream = fileUploading.Stream
			};

			try
			{
				await _s3Client.PutObjectAsync(request);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DownloadFileFromS3BucketAsync(string fileName, string currentUserEmail)
		{
			var request = new GetObjectRequest()
			{
				BucketName = _bucketName,
				Key = $"{currentUserEmail}/{fileName}"
			};

			using GetObjectResponse response = await _s3Client.GetObjectAsync(request);
			await response.WriteResponseStreamToFileAsync(
				$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{fileName}", true, CancellationToken.None);

			return true;
		}

		public async Task<bool> DeleteFileFromS3BucketAsync(string fileName, string currentUserEmail)
		{
			var request = new DeleteObjectRequest()
			{
				BucketName = _bucketName,
				Key = $"{currentUserEmail}/{fileName}"
			};

			await _s3Client.DeleteObjectAsync(request);

			return true;
		}
	}
}
