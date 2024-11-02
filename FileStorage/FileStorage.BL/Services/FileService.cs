using FileStorage.DAL.UnitOfWork;
using FileStorage.DAL;
using FileStorage.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.BL.Services
{
    public class FileService
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

    }
}
