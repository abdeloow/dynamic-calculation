using CosmenticFormulaApp.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CosmenticFormulaApp.Web.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<FormulaUpdateHub, IFormulaUpdateClient> _hubContext;

        public SignalRService(IHubContext<FormulaUpdateHub, IFormulaUpdateClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyFormulaHighlight(List<int> formulaIds)
        {
            await _hubContext.Clients.Group("FormulaUpdates").FormulaHighlighted(formulaIds);
        }
        public async Task NotifyImportComplete(string message, bool success)
        {
            await _hubContext.Clients.Group("FormulaUpdates").ImportCompleted(message, success);
        }

        public async Task NotifyPriceUpdate(int rawMaterialId, List<int> affectedFormulaIds)
        {
            await _hubContext.Clients.Group("FormulaUpdates").PriceUpdated(rawMaterialId, affectedFormulaIds);
        }
    }
}
