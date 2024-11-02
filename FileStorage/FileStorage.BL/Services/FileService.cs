using FileStorage.DAL.UnitOfWork;
using FileStorage.DAL;
using FileStorage.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.BL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FileStorage.BL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStoredFileRepository _storedFileRepository;
        private readonly IStoredFileDetailsRepository _storedFileDetailsRepository;
        public FileService(IStoredFileRepository storedFileRepository, IStoredFileDetailsRepository storedFileDetailsRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _storedFileRepository = storedFileRepository;
            _storedFileDetailsRepository = storedFileDetailsRepository;
        }


        public async Task SaveFileLocalAsync(IFormFile file, User user, bool isPublic)
        {
            // Определяем путь для хранения файла
            var rootUploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var userFolderPath = Path.Combine(rootUploadsPath, user.Name); // Можно использовать также user.Id
            var targetFolder = isPublic ? "Public" : "Private";
            var targetFolderPath = Path.Combine(userFolderPath, targetFolder);

            // Создаём папку, если её ещё нет
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            // Полный путь для сохранения файла
            var filePath = Path.Combine(targetFolderPath, file.FileName);

            // Сохраняем файл в файловую систему
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Заполняем данные для StoredFile
            var storedFile = new StoredFile
            {
                Name = file.FileName,
                Type = file.ContentType,
                Size = file.Length / 1024.0 / 1024.0, // Размер в мегабайтах
                Path = filePath,
                UserId = user.Id
            };

            // Заполняем данные для StoredFileDetails
            var storedFileDetails = new StoredFileDetails
            {
                IsPublic = isPublic,
                UploadDateTime = DateTime.UtcNow,
                ExpireDateTime = DateTime.UtcNow.AddYears(1), // Условная дата истечения
                StoredFile = storedFile
            };

            // Сохраняем данные в базу данных
            await _unitOfWork.StoredFiles.CreateAsync(storedFile);
            await _unitOfWork.StoredFilesDetails.CreateAsync(storedFileDetails);


            await _unitOfWork.CommitAsync();
        }
    }
}
