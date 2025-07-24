namespace Finance.Application.DTOs;

public class StoredFile
{
    public List<string> Path { get; set; }

    public string? FileName { get; set; }
    public long FileSizeInBytes { get; set; }
    public DateTimeOffset UploadedDate { get; set; }
}
