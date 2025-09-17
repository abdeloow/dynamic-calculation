namespace CosmenticFormulaApp.Web.Models
{
    public class UploadResult
    {
        public string FileName { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
