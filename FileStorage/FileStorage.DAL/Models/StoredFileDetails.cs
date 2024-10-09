namespace FileStorage.DAL.Models
{
	public class StoredFileDetails
	{
		public int Id { get; set; }
		public bool IsPublic { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime UploadDateTime { get; set; }
		public DateTime ExpireDateTime { get; set; }

		public int StoredFileId { get; set; }
		public StoredFile StoredFile { get; set; } = null!;
	}
}