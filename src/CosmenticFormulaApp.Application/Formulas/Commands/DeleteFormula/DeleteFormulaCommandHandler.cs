using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Domain.Events;
using CosmenticFormulaApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.DeleteFormula
{
    public class DeleteFormulaCommandHandler : IRequestHandler<DeleteFormulaCommand, Result>
    {
        private readonly IFormulaRepository _formulaRepository;
        public DeleteFormulaCommandHandler(IFormulaRepository formulaRepository)
        {
            _formulaRepository = formulaRepository;
        }
        public async Task<Result> Handle(DeleteFormulaCommand request, CancellationToken cancellationToken)
        {
            var formula = await _formulaRepository.GetByIdAsync(request.FormulaId);
            if (formula == null)
                return Result.Failure($"Formula with ID {request.FormulaId} not found");
            formula.AddDomainEvent(new FormulaDeletedEvent(formula.Id, formula.Name, DateTime.UtcNow));
            await _formulaRepository.DeleteAsync(request.FormulaId);
            return Result.Success();
        }
    }
}
