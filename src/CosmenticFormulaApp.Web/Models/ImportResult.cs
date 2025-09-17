namespace CosmenticFormulaApp.Web.Models;

public class ImportResult
{
    public string FileName { get; set; } = "";
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public int? FormulaId { get; set; }
}