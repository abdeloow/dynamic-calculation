using Microsoft.AspNetCore.Mvc;
using MediatR;
using CosmenticFormulaApp.Domain.Enums;
using CosmenticFormulaApp.Application.Formulas.Queries.GetSubstanceAnalysis;

namespace CosmenticFormulaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnalysisController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get substance usage analysis
    /// </summary>
    [HttpGet("substances")]
    public async Task<IActionResult> GetSubstanceAnalysis(
        [FromQuery] SortBy sortBy = SortBy.TotalWeight,
        [FromQuery] bool ascending = false)
    {
        var query = new GetSubstanceAnalysisQuery
        {
            SortBy = sortBy,
            Ascending = ascending,
            IncludeFormulaCount = true
        };

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
            return Ok(result.Data);

        return BadRequest(result.Error);
    }
}