namespace FileStorage.WebApi.DTOs
{
	public class StoredFileDetailsDTO
	{
		public int StoredFileId { get; set; }
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;
		public double FileSize { get; set; }
		public string FilePath { get; set; } = null!;

		
		public int StoredFileDetailsId { get; set; }
		public bool isPublic { get; set; } = false;
		public bool IsDeleted { get; set; } = false;
		public DateTime UploadDateTime { get; set; }
		public DateTime ExpireDateTime { get; set; }
	}
}
