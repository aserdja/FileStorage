namespace FileStorage.WebApi.DTOs
{
    public class StoredFileDto
    {
        public string FileName { get; set; } = null!;
        public int UserId { get; set; }
        public double Size { get; set; }
        public string Type { get; set; } = null!;
    }
}
