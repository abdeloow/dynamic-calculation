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
    public class FormulaImportedEventHandler : INotificationHandler<FormulaImportedEvent>
    {
        private readonly ILogger<FormulaImportedEventHandler> _logger;

        public FormulaImportedEventHandler(ILogger<FormulaImportedEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(FormulaImportedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Formula imported: {FormulaName} (ID: {FormulaId}) via {Source}",
                notification.FormulaName, notification.FormulaId, notification.Source);

            await Task.CompletedTask;
        }
    }
}
