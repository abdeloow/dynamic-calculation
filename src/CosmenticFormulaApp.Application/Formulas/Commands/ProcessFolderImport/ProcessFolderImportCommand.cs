using CosmenticFormulaApp.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.ProcessFolderImport
{
    public class ProcessFolderImportCommand : IRequest<Result<ImportBatchResult>>
    {
        public List<string> FilePaths { get; set; } = new();
    }
}
