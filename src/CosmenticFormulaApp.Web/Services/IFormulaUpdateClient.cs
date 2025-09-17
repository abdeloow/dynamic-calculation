namespace CosmenticFormulaApp.Web.Services
{
    public interface IFormulaUpdateClient
    {
        Task FormulaHighlighted(List<int> formulaIds);
        Task FormulaImported(int formulaId, string formulaName);
        Task ImportCompleted(string message, bool success);
        Task PriceUpdated(int rawMaterialId, List<int> affectedFormulaIds);
    }
}
