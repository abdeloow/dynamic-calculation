using Microsoft.AspNetCore.Mvc;
using MediatR;
using CosmenticFormulaApp.Application.Formulas.Commands.ImportFormula;
using CosmenticFormulaApp.Application.Formulas.Commands.DeleteFormula;
using CosmenticFormulaApp.Application.Formulas.Queries.GetFormulasList;
using CosmenticFormulaApp.Application.DTOs.Input;

namespace CosmenticFormulaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormulasController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormulasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all formulas with optional filtering and sorting
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetFormulas(
        [FromQuery] string? nameFilter = null,
        [FromQuery] string sortBy = "Name",
        [FromQuery] bool ascending = true)
    {
        var query = new GetFormulasListQuery
        {
            NameFilter = nameFilter,
            SortBy = sortBy,
            Ascending = ascending
        };

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
            return Ok(result.Data);

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Import a new formula from JSON
    /// </summary>
    [HttpPost("import")]
    public async Task<IActionResult> ImportFormula([FromBody] ImportFormulaRequest request)
    {
        var command = new ImportFormulaCommand
        {
            JsonContent = request.JsonContent,
            FileName = request.FileName,
            Source = request.Source
        };

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new { FormulaId = result.Data, Message = "Formula imported successfully" });

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Import multiple formulas from file upload
    /// </summary>
    [HttpPost("import/batch")]
    public async Task<IActionResult> ImportFormulaBatch(IFormFileCollection files)
    {
        var importResults = new List<object>();

        foreach (var file in files)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var jsonContent = await reader.ReadToEndAsync();

                var command = new ImportFormulaCommand
                {
                    JsonContent = jsonContent,
                    FileName = file.FileName,
                    Source = CosmenticFormulaApp.Domain.Enums.ImportSource.Manual
                };

                var result = await _mediator.Send(command);

                importResults.Add(new
                {
                    FileName = file.FileName,
                    Success = result.IsSuccess,
                    Message = result.IsSuccess ? "Imported successfully" : result.Error,
                    FormulaId = result.IsSuccess ? result.Data : (int?)null
                });
            }
            catch (Exception ex)
            {
                importResults.Add(new
                {
                    FileName = file.FileName,
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    FormulaId = (int?)null
                });
            }
        }

        return Ok(importResults);
    }

    /// <summary>
    /// Delete a formula by ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormula(int id)
    {
        var command = new DeleteFormulaCommand { FormulaId = id };
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new { Message = "Formula deleted successfully" });

        return BadRequest(result.Error);
    }
}

public class ImportFormulaRequest
{
    public string JsonContent { get; set; } = "";
    public string FileName { get; set; } = "";
    public CosmenticFormulaApp.Domain.Enums.ImportSource Source { get; set; }
}