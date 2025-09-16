using Microsoft.AspNetCore.Mvc;
using MediatR;
using CosmenticFormulaApp.Application.Formulas.Queries.GetRawMaterialsList;
using CosmenticFormulaApp.Application.Formulas.Commands.UpdateRawMaterialPrice;

namespace CosmenticFormulaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RawMaterialsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RawMaterialsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all raw materials
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetRawMaterials()
    {
        var query = new GetRawMaterialsListQuery();
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
            return Ok(result.Data);

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Update raw material price
    /// </summary>
    [HttpPut("{id}/price")]
    public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceRequest request)
    {
        var command = new UpdateRawMaterialPriceCommand
        {
            RawMaterialId = id,
            NewPriceAmount = request.NewPriceAmount,
            Currency = request.Currency,
            ReferenceUnit = request.ReferenceUnit
        };

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new
            {
                Message = "Price updated successfully",
                AffectedFormulaIds = result.Data
            });

        return BadRequest(result.Error);
    }
}

public class UpdatePriceRequest
{
    public decimal NewPriceAmount { get; set; }
    public string Currency { get; set; } = "EUR";
    public string ReferenceUnit { get; set; } = "kg";
}