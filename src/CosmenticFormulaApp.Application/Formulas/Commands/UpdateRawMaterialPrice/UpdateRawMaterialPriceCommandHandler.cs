using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.UpdateRawMaterialPrice
{
    public class UpdateRawMaterialPriceCommandHandler : IRequestHandler<UpdateRawMaterialPriceCommand, Result<List<int>>>
    {
        private readonly IRawMaterialRepository _rawMaterialRepository;
        private readonly IFormulaRepository _formulaRepository;
        private readonly ICostCalculationService _costCalculationService;
        public UpdateRawMaterialPriceCommandHandler(
            IRawMaterialRepository rawMaterialRepository,
            IFormulaRepository formulaRepository,
            ICostCalculationService costCalculationService)
        {
            _rawMaterialRepository = rawMaterialRepository;
            _formulaRepository = formulaRepository;
            _costCalculationService = costCalculationService;
        }
        public async Task<Result<List<int>>> Handle(UpdateRawMaterialPriceCommand request, CancellationToken cancellationToken)
        {
            var rawMaterial = await _rawMaterialRepository.GetByIdAsync(request.RawMaterialId);
            if (rawMaterial == null)
                return Result<List<int>>.Failure($"Raw material with ID {request.RawMaterialId} not found");
            rawMaterial.UpdatePrice(request.NewPriceAmount, request.Currency, request.ReferenceUnit);
            await _rawMaterialRepository.AddOrUpdateAsync(rawMaterial);

            var affectedFormulas = await _formulaRepository.GetFormulasUsingRawMaterialAsync(request.RawMaterialId);
            var affectedFormulaIds = new List<int>();

            foreach (var formula in affectedFormulas)
            {
                _costCalculationService.RecalculateFormulaCost(formula);
                formula.SetHighlighted(true);
                await _formulaRepository.UpdateAsync(formula);
                affectedFormulaIds.Add(formula.Id);
            }
            return Result<List<int>>.Success(affectedFormulaIds);
        }
    }
}
