namespace FileStorage.DAL.Models
{
	public class StoredFile
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Type { get; set; } = null!;
		public double Size { get; set; }
		public string Path { get; set; } = null!;

		public int StoredFileDetailsId { get; set; }
		public StoredFileDetails StoredFilesDetails { get; set; } = null!;

		public int UserId { get; set; }
		public User User { get; set; } = null!;
	}
}