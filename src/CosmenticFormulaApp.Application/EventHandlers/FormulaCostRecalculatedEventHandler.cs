using CosmenticFormulaApp.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.EventHandlers
{
    public class FormulaCostRecalculatedEventHandler : INotificationHandler<FormulaCostRecalculatedEvent>
    {
        private readonly ILogger<FormulaCostRecalculatedEventHandler> _logger;

        public FormulaCostRecalculatedEventHandler(ILogger<FormulaCostRecalculatedEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(FormulaCostRecalculatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Formula cost recalculated: {FormulaName} (ID: {FormulaId}) from {OldCost:F2} to {NewCost:F2} EUR",
                notification.FormulaName,
                notification.FormulaId,
                notification.OldCost,
                notification.NewCost);

            await Task.CompletedTask;
        }
    }
}
