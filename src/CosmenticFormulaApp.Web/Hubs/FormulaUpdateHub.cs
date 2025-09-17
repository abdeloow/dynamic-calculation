using CosmenticFormulaApp.Web.Services;
using Microsoft.AspNetCore.SignalR;

namespace CosmenticFormulaApp.Web.Hubs
{
    public class FormulaUpdateHub : Hub<IFormulaUpdateClient>
    {
        public async Task JoinFormulaUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "FormulaUpdates");
        }
        public async Task LeaveFormulaUpdates()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "FormulaUpdates");
        }
    }
}
