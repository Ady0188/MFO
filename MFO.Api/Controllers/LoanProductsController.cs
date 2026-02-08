using MediatR;
using MFO.Application.Common.Models;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.LoanProducts.Commands;
using MFO.Application.ReferenceData.LoanProducts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/loan-products")]
public sealed class LoanProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoanProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoanProductsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetLoanProductByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LoanProductDto>> Create(LoanProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateLoanProductCommand(request), cancellationToken);
        return MapCommandResult(result, nameof(GetById));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanProductDto>> Update(Guid id, LoanProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateLoanProductCommand(id, request), cancellationToken);
        return MapCommandResult(result, null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanProductCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private ActionResult<LoanProductDto> MapCommandResult(CommandResult<LoanProductDto> result, string? createdAtAction)
    {
        if (result.Error == CommandError.NotFound)
        {
            return NotFound();
        }

        if (result.Error == CommandError.MissingReferences)
        {
            return BadRequest($"Missing references: {string.Join(", ", result.MissingReferences)}");
        }

        if (result.Value is null)
        {
            return BadRequest();
        }

        return createdAtAction is null
            ? Ok(result.Value)
            : CreatedAtAction(createdAtAction, new { id = result.Value.Id }, result.Value);
    }
}
