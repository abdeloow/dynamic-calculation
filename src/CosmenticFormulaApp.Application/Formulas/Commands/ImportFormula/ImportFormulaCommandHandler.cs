using CosmenticFormulaApp.Application.Common.Interfaces;
using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Input;
using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Domain.Services;
using CosmenticFormulaApp.Domain.Models;
using MediatR;


namespace CosmenticFormulaApp.Application.Formulas.Commands.ImportFormula
{
    public class ImportFormulaCommandHandler : IRequestHandler<ImportFormulaCommand, Result<int>>
    {
        private readonly IJsonParsingService _jsonParsingService;
        private readonly IFormulaRepository _formulaRepository;
        private readonly IRawMaterialRepository _rawMaterialRepository;
        private readonly ISubstanceRepository _substanceRepository;
        private readonly ICostCalculationService _costCalculationService;
        private readonly IFormulaValidationService _validationService;

        public ImportFormulaCommandHandler(
            IJsonParsingService jsonParsingService,
            IFormulaRepository formulaRepository,
            IRawMaterialRepository rawMaterialRepository,
            ISubstanceRepository substanceRepository,
            ICostCalculationService costCalculationService,
            IFormulaValidationService validationService)
        {
            _jsonParsingService = jsonParsingService;
            _formulaRepository = formulaRepository;
            _rawMaterialRepository = rawMaterialRepository;
            _substanceRepository = substanceRepository;
            _costCalculationService = costCalculationService;
            _validationService = validationService;
        }
        public async Task<Result<int>> Handle(ImportFormulaCommand request, CancellationToken cancellationToken)
        {
            var parseResult = await _jsonParsingService.ParseFormulaAsync(request.JsonContent);
            if (!parseResult.IsSuccess)
                return Result<int>.Failure(parseResult.Error);
            var formulaDto = parseResult.Data;
            var existingFormulas = await _formulaRepository.GetAllAsync();
            var nameValidation = _validationService.ValidateUniqueFormulaName(formulaDto.Name, existingFormulas);
            if (!nameValidation.IsSuccess)
                return Result<int>.Failure(nameValidation.Message);
            var formula = Formula.Create(formulaDto.Name, formulaDto.Weight, formulaDto.WeightUnit);
            foreach (var rawMaterialDto in formulaDto.RawMaterials)
            {
                var rawMaterial = await GetOrCreateRawMaterial(rawMaterialDto);
                await ProcessRawMaterialSubstances(rawMaterial, rawMaterialDto.Substances);

                formula.AddRawMaterial(rawMaterial, rawMaterialDto.Percentage);
            }
            var formulaValidation = _validationService.ValidateFormula(formula);
            if (!formulaValidation.IsSuccess)
                return Result<int>.Failure(formulaValidation.Errors);

            _costCalculationService.RecalculateFormulaCost(formula);

            var savedFormula = await _formulaRepository.AddAsync(formula);
            return Result<int>.Success(savedFormula.Id);
        }
        private async Task<RawMaterial> GetOrCreateRawMaterial(RawMaterialDto dto)
        {
            var existing = await _rawMaterialRepository.GetByNameAsync(dto.Name);
            if (existing != null)
            {
                if (existing.Price.Amount != dto.Price.Amount)
                {
                    existing.UpdatePrice(dto.Price.Amount, dto.Price.Currency, dto.Price.ReferenceUnit);
                    await _rawMaterialRepository.AddOrUpdateAsync(existing);
                }
                return existing;
            }

            return RawMaterial.Create(dto.Name, dto.Price.Amount, dto.Price.Currency, dto.Price.ReferenceUnit);
        }
        private async Task ProcessRawMaterialSubstances(RawMaterial rawMaterial, List<SubstanceDto> substanceDtos)
        {
            foreach (var substanceDto in substanceDtos)
            {
                var substance = await GetOrCreateSubstance(substanceDto.Name);
                rawMaterial.AddSubstance(substance, substanceDto.Percentage);
            }
        }
        private async Task<Substance> GetOrCreateSubstance(string name)
        {
            var existing = await _substanceRepository.GetByNameAsync(name);
            return existing ?? Substance.Create(name);
        }
    }
}
