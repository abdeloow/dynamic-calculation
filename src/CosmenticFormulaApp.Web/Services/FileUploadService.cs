using CosmenticFormulaApp.Web.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace CosmenticFormulaApp.Web.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ILogger<FileUploadService> _logger;
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public FileUploadService(ILogger<FileUploadService> logger)
        {
            _logger = logger;
        }
        public async Task<List<UploadResult>> ProcessFilesAsync(List<IBrowserFile> files)
        {
            var results = new List<UploadResult>();
            foreach (var file in files)
            {
                try
                {
                    _logger.LogInformation("Processing file: {FileName}", file.Name);

                    // Validate file
                    if (file.Size > MaxFileSize)
                    {
                        results.Add(new UploadResult
                        {
                            FileName = file.Name,
                            Success = false,
                            Message = $"File size ({file.Size:N0} bytes) exceeds maximum allowed size ({MaxFileSize:N0} bytes)"
                        });
                        continue;
                    }
                    if (!file.Name.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(new UploadResult
                        {
                            FileName = file.Name,
                            Success = false,
                            Message = "Only JSON files are allowed"
                        });
                        continue;
                    }
                    using var stream = file.OpenReadStream(MaxFileSize);
                    using var reader = new StreamReader(stream);
                    var content = await reader.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        results.Add(new UploadResult
                        {
                            FileName = file.Name,
                            Success = false,
                            Message = "File appears to be empty"
                        });
                        continue;
                    }
                    results.Add(new UploadResult
                    {
                        FileName = file.Name,
                        Success = true,
                        Message = "File processed successfully",
                        Content = content
                    });
                    _logger.LogInformation("Successfully processed file: {FileName}, Size: {Size} bytes",
                        file.Name, content.Length);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing file {FileName}", file.Name);
                    results.Add(new UploadResult
                    {
                        FileName = file.Name,
                        Success = false,
                        Message = $"Error processing file: {ex.Message}"
                    });
                }
            }
            return results;
        }
    }
}
