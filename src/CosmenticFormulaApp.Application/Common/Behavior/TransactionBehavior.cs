using CosmenticFormulaApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Common.Behavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IApplicationDbContext context, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (typeof(TRequest).Name.EndsWith("Query"))
            {
                return await next();
            }
            _logger.LogInformation("Beginning transaction for {RequestName}", typeof(TRequest).Name);

            await _context.BeginTransactionAsync();

            try
            {
                var response = await next();
                await _context.CommitTransactionAsync();

                _logger.LogInformation("Transaction committed for {RequestName}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction failed for {RequestName}, rolling back", typeof(TRequest).Name);
                await _context.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
