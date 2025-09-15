using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetAffectedFormulas
{
    public class GetAffectedFormulasQueryHandler : IRequestHandler<GetAffectedFormulasQuery, Result<List<int>>>
    {
        private readonly IFormulaRepository _formulaRepository;

        public GetAffectedFormulasQueryHandler(IFormulaRepository formulaRepository)
        {
            _formulaRepository = formulaRepository;
        }
        public async Task<Result<List<int>>> Handle(GetAffectedFormulasQuery request, CancellationToken cancellationToken)
        {
            var affectedFormulas = await _formulaRepository.GetFormulasUsingRawMaterialAsync(request.RawMaterialId);
            var formulaIds = affectedFormulas.Select(f => f.Id).ToList();

            return Result<List<int>>.Success(formulaIds);
        }
    }
}
