using Amazon.S3;
using Amazon.S3.Model;
using FileStorage.BL.Models;
using FileStorage.BL.Services.FileServices.Interfaces;

namespace FileStorage.BL.Services.FileServices
{
	public class S3BucketService(IAmazonS3 s3Client) : IS3BucketService
	{
		private readonly IAmazonS3 _s3Client = s3Client;
		private readonly string _bucketName = "filestorage.s3bucket";

		public async Task<bool> UploadFileToS3BucketAsync(FileUploading fileUploading, string currentUserEmail)
		{
			try
			{
				var request = new PutObjectRequest()
				{
					BucketName = _bucketName,
					Key = $"{currentUserEmail}/{fileUploading.FileName}",
					InputStream = fileUploading.Stream
				};

				await _s3Client.PutObjectAsync(request);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
