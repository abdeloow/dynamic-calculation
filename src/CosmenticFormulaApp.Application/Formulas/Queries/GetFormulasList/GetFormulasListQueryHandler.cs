using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using CosmenticFormulaApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetFormulasList
{
    public class GetFormulasListQueryHandler : IRequestHandler<GetFormulasListQuery, Result<List<FormulaListItemDto>>>
    {
        private readonly IFormulaRepository _formulaRepository;

        public GetFormulasListQueryHandler(IFormulaRepository formulaRepository)
        {
            _formulaRepository = formulaRepository;
        }

        public async Task<Result<List<FormulaListItemDto>>> Handle(GetFormulasListQuery request, CancellationToken cancellationToken)
        {
            var formulas = await _formulaRepository.GetAllAsync();

            var formulaDtos = formulas.Select(f => new FormulaListItemDto
            {
                Id = f.Id,
                Name = f.Name,
                Weight = f.Weight,
                WeightUnit = f.WeightUnit,
                TotalCost = f.TotalCost,
                IsHighlighted = f.IsHighlighted,
                CreatedAt = f.CreatedAt
            }).ToList();

            if (!string.IsNullOrWhiteSpace(request.NameFilter))
            {
                formulaDtos = formulaDtos.Where(f => f.Name.Contains(request.NameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            formulaDtos = request.SortBy.ToLowerInvariant() switch
            {
                "weight" => request.Ascending ? formulaDtos.OrderBy(f => f.Weight).ToList() : formulaDtos.OrderByDescending(f => f.Weight).ToList(),
                "totalcost" => request.Ascending ? formulaDtos.OrderBy(f => f.TotalCost).ToList() : formulaDtos.OrderByDescending(f => f.TotalCost).ToList(),
                "createdat" => request.Ascending ? formulaDtos.OrderBy(f => f.CreatedAt).ToList() : formulaDtos.OrderByDescending(f => f.CreatedAt).ToList(),
                _ => request.Ascending ? formulaDtos.OrderBy(f => f.Name).ToList() : formulaDtos.OrderByDescending(f => f.Name).ToList()
            };

            return Result<List<FormulaListItemDto>>.Success(formulaDtos);
        }
    }
}
