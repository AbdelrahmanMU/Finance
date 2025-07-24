namespace Finance.Shared.ApplicationFiles;

public class FileDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Url { get; set; } = default!;
    public bool IsExternal { get; set; }
    public string? FileName { get; set; }
    public string? FileExtension { get; set; }
    public long? FileSize { get; set; }
}
