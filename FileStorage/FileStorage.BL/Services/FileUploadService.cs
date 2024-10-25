using FileStorage.BL.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.BL.Services
{
    public class FileUploadService : IFileUploadService
    {

            private readonly IStoredFileRepository _storedFileRepository;

        public FileUploadService(IStoredFileRepository storedFileRepository)
        {
            _storedFileRepository = storedFileRepository;
        }

        public async Task CreateAsync(string fileName, int userId, double size, string type, CancellationToken cancellationToken = default)
        {
            var storedFile = new StoredFile
            {
                Name = fileName,
                UserId = userId,
                Size = size,
                Type = type
            };

            await _storedFileRepository.CreateAsync(storedFile, cancellationToken);
        }
    }
}
