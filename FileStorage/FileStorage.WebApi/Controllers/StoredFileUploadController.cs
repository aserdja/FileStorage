
using Microsoft.AspNetCore.Mvc;
using FileStorage.WebApi.DTOs;
using FileStorage.BL.Interfaces;
[ApiController]
[Route("api/[controller]")]
public class StoredFileUploadController : ControllerBase
{

    private const long MaxFileSize = 2000 * 1024 * 1024;  // максимальный размер загружаемого файла 

    private readonly IFileUploadService _fileUploadService;

    public StoredFileUploadController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }



    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] StoredFileDto dto)
    {
        await _fileUploadService.CreateAsync(dto.FileName, dto.UserId, dto.Size, dto.Type);
        return NoContent();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // получаем информацию от пользователя загрузившего файл 



        // загружаем файл 

        if (file == null || file.Length == 0)
        {
            return BadRequest("Empty File");
        }
        if (file.Length > MaxFileSize)
        {
            return BadRequest($"файл не дольжен быть больше {MaxFileSize / 1024 / 1024} Mb");
        }
        else
        {

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }


    }
}


        //
    //    [HttpPost("upload")]
    //public async Task<IActionResult> UploadFile(IFormFile file)
    //{
    //    // Получаем информацию от пользователя, загрузившего файл
    //    var userName = User.Identity?.Name;

    //    if (userName == null)
    //    {
    //        return Unauthorized("Пользователь не аутентифицирован.");
    //    }

    //    // Найти пользователя по имени
    //    var user = await _userService.GetByLoginAsync(userName);
    //    if (user == null)
    //    {
    //        return BadRequest("Пользователь не найден.");
    //    }

    //    // Загружаем файл
    //    if (file == null || file.Length == 0)
    //    {
    //        return BadRequest("Файл не загружен или пуст.");
    //    }

    //    if (file.Length > MaxFileSize)
    //    {
    //        return BadRequest($"Файл не должен быть больше {MaxFileSize / 1024 / 1024} MB.");
    //    }

    //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
    //    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    //    if (!Directory.Exists(uploadsFolderPath))
    //    {
    //        Directory.CreateDirectory(uploadsFolderPath);
    //    }

    //    using (var stream = new FileStream(filePath, FileMode.Create))
    //    {
    //        await file.CopyToAsync(stream);
    //    }

    //    // Дополнительная логика: сохранение файла и данных о пользователе в базе данных
    //    await _fileUploadService.CreateAsync(file.FileName, user.Id, file.Length / 1024.0 / 1024.0, file.ContentType);

    //    return Ok(new { message = "Файл успешно загружен", fileName = file.FileName, uploadedBy = userName });
