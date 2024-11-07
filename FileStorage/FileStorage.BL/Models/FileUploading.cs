using System.ComponentModel.DataAnnotations;

namespace FileStorage.BL.Models
{
	public class FileUploading
	{
		[Required]
		public string FileName { get; set; } = string.Empty;

		[Required]
		public string ContentType {  get; set; } = string.Empty;

		[Required]
		public string S3Path { get; set; } = string.Empty;

		[Required]
		public long Length { get; set; }

		[Required]
		public Stream Stream { get; set; } = null!;
	}
}
