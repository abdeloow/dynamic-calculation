using CosmenticFormulaApp.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.DeleteFormula
{
    public class DeleteFormulaCommand : IRequest<Result>
    {
        public int FormulaId { get; set; }
    }
}
