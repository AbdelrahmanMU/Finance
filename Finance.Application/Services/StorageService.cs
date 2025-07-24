using Finance.Application.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Services;

public class StorageService : IStorageService
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public StorageService(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<StoredFile> Upload(IFormFile file, bool deleteOldFiles = false, params string[] folders)
    {
        var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, Path.Combine(folders));

        CreateDirectoryIfNotExist(dirPath);
        if (deleteOldFiles) DeleteFolderFiles(dirPath);

        await using var filestream = File.Create(Path.Combine(dirPath, file.FileName));
        await file.CopyToAsync(filestream);
        await filestream.FlushAsync();

        var fullPath = folders.ToList();
        fullPath.Add(file.FileName);

        var storedFile = new StoredFile
        {
            Path = fullPath,
            FileName = file.FileName,
            FileSizeInBytes = file.Length,
            UploadedDate = DateTimeOffset.Now
        };

        return storedFile;
    }

    private static void CreateDirectoryIfNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
    }

    private static void DeleteFolderFiles(string folderPath)
    {
        Array.ForEach(Directory.GetFiles(folderPath), delegate (string filePath) { File.Delete(filePath); });
    }
}
