using CosmenticFormulaApp.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetAffectedFormulas
{
    public class GetAffectedFormulasQuery : IRequest<Result<List<int>>>
    {
        public int RawMaterialId { get; set; }
    }
}
