using Microsoft.AspNetCore.Http;

namespace TetPee.service.MediaService;

public interface IService
{
    public Task<string> UploadImageAsync(IFormFile file);
}