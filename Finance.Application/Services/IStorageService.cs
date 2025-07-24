using Finance.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Services;

public interface IStorageService
{
    Task<StoredFile> Upload(IFormFile file, bool deleteOldFiles = false, params string[] folders);
}
