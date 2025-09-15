using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using CosmenticFormulaApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetRawMaterialsList
{
    public class GetRawMaterialsListQueryHandler : IRequestHandler<GetRawMaterialsListQuery, Result<List<RawMaterialListItemDto>>>
    {
        private readonly IRawMaterialRepository _rawMaterialRepository;

        public GetRawMaterialsListQueryHandler(IRawMaterialRepository rawMaterialRepository)
        {
            _rawMaterialRepository = rawMaterialRepository;
        }

        public async Task<Result<List<RawMaterialListItemDto>>> Handle(GetRawMaterialsListQuery request, CancellationToken cancellationToken)
        {
            var rawMaterials = await _rawMaterialRepository.GetAllAsync();

            var rawMaterialDtos = rawMaterials.Select(rm => new RawMaterialListItemDto
            {
                Id = rm.Id,
                Name = rm.Name,
                PriceAmount = rm.Price.Amount,
                PriceCurrency = rm.Price.Currency,
                PriceReferenceUnit = rm.Price.ReferenceUnit,
                UsedInFormulasCount = rm.FormulaRawMaterials.Count
            }).ToList();

            return Result<List<RawMaterialListItemDto>>.Success(rawMaterialDtos);
        }
    }
}
