using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetSubstanceAnalysis
{
    public class GetSubstanceAnalysisQueryHandler : IRequestHandler<GetSubstanceAnalysisQuery, Result<List<SubstanceAnalysisDto>>>
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly ISubstanceAnalysisService _substanceAnalysisService;

        public GetSubstanceAnalysisQueryHandler(
            IFormulaRepository formulaRepository,
            ISubstanceAnalysisService substanceAnalysisService)
        {
            _formulaRepository = formulaRepository;
            _substanceAnalysisService = substanceAnalysisService;
        }

        public async Task<Result<List<SubstanceAnalysisDto>>> Handle(GetSubstanceAnalysisQuery request, CancellationToken cancellationToken)
        {
            var allFormulas = await _formulaRepository.GetAllAsync();
            var analysis = _substanceAnalysisService.GetMostUsedSubstances(allFormulas, request.SortBy);

            var analysisDtos = analysis.Select(a => new SubstanceAnalysisDto
            {
                Name = a.Name,
                TotalWeight = a.TotalWeight,
                FormulaCount = request.IncludeFormulaCount ? a.FormulaCount : 0
            }).ToList();

            if (request.Ascending)
            {
                analysisDtos.Reverse();
            }

            return Result<List<SubstanceAnalysisDto>>.Success(analysisDtos);
        }
    }
}
