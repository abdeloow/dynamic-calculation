namespace CosmenticFormulaApp.Web.Services
{
    public interface ISignalRService
    {
        Task NotifyFormulaHighlight(List<int> formulaIds);
        Task NotifyImportComplete(string message, bool success);
        Task NotifyPriceUpdate(int rawMaterialId, List<int> affectedFormulaIds);
    }
}
