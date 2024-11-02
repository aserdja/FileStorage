using FileStorage.DAL.Models;
using Microsoft.AspNetCore.Http;
namespace FileStorage.BL.Interfaces
{
    internal interface IFileService
    {

        Task SaveFileLocalAsync(IFormFile file, User user, bool isPublic);
    }
}
