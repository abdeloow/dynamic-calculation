using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.Formulas.Commands.ImportFormula;
using CosmenticFormulaApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.ProcessFolderImport
{
    public class ProcessFolderImportCommandHandler : IRequestHandler<ProcessFolderImportCommand, Result<ImportBatchResult>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessFolderImportCommandHandler> _logger;
        public ProcessFolderImportCommandHandler(IMediator mediator, ILogger<ProcessFolderImportCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<Result<ImportBatchResult>> Handle(ProcessFolderImportCommand request, CancellationToken cancellationToken)
        {
            var result = new ImportBatchResult();

            foreach (var filePath in request.FilePaths)
            {
                try
                {
                    var jsonContent = await File.ReadAllTextAsync(filePath, cancellationToken);
                    var fileName = Path.GetFileName(filePath);

                    var importCommand = new ImportFormulaCommand
                    {
                        JsonContent = jsonContent,
                        FileName = fileName,
                        Source = ImportSource.Automatic
                    };

                    var importResult = await _mediator.Send(importCommand, cancellationToken);

                    if (importResult.IsSuccess)
                    {
                        result.SuccessCount++;
                        result.ImportedFormulaIds.Add(importResult.Data);
                        _logger.LogInformation("Successfully imported formula from {FileName}", fileName);
                    }
                    else
                    {
                        result.ErrorCount++;
                        result.Errors.Add(new ImportError
                        {
                            FileName = fileName,
                            ErrorMessage = importResult.Error,
                            ValidationErrors = importResult.Errors
                        });
                        _logger.LogWarning("Failed to import formula from {FileName}: {Error}", fileName, importResult.Error);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorCount++;
                    result.Errors.Add(new ImportError
                    {
                        FileName = Path.GetFileName(filePath),
                        ErrorMessage = $"Unexpected error: {ex.Message}"
                    });
                    _logger.LogError(ex, "Unexpected error importing file {FilePath}", filePath);
                }
            }
            return Result<ImportBatchResult>.Success(result);
        }
    }
}
