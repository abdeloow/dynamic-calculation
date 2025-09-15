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
    public class RawMaterialPriceUpdatedEventHandler : INotificationHandler<RawMaterialPriceUpdatedEvent>
    {
        private readonly ILogger<RawMaterialPriceUpdatedEventHandler> _logger;

        public RawMaterialPriceUpdatedEventHandler(ILogger<RawMaterialPriceUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(RawMaterialPriceUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Raw material price updated: {RawMaterialName} (ID: {RawMaterialId}) from {OldPrice} to {NewPrice}",
                notification.RawMaterialName,
                notification.RawMaterialId,
                notification.OldPrice.FormattedPrice,
                notification.NewPrice.FormattedPrice);

            await Task.CompletedTask;
        }
    }
}
