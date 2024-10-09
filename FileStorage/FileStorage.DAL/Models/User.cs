namespace FileStorage.DAL.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Login { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;

		public ICollection<StoredFile>? StoredFiles { get; set; }
	}
}
