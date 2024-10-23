using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BL.Interfaces
{
    public interface IFileUploadService
    {
        Task CreateAsync(string fileName, int userId, double size, string type, CancellationToken cancellationToken = default);
    }
}
