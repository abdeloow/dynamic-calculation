using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.ImportFormula
{
    public class ImportFormulaCommand : IRequest<Result<int>>
    {
        public string JsonContent { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public ImportSource Source { get; set; } = ImportSource.Manual;
    }
}
