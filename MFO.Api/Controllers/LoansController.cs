using MediatR;
using MFO.Application.Common.Models;
using MFO.Application.Loans;
using MFO.Application.Loans.Commands;
using MFO.Application.Loans.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/loans")]
public sealed class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoansQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var loan = await _mediator.Send(new GetLoanByIdQuery(id), cancellationToken);
        if (loan is null)
        {
            return NotFound();
        }

        return Ok(loan);
    }

    [HttpPost]
    public async Task<ActionResult<LoanDto>> Create(CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateLoanCommand(request), cancellationToken);
        return MapCommandResult(result, nameof(GetById));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanDto>> Update(Guid id, UpdateLoanRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateLoanCommand(id, request), cancellationToken);
        return MapCommandResult(result, null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private ActionResult<LoanDto> MapCommandResult(CommandResult<LoanDto> result, string? createdAtAction)
    {
        if (result.Error == CommandError.NotFound)
        {
            return NotFound();
        }

        if (result.Error == CommandError.Conflict)
        {
            return Conflict("Loan number already exists.");
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
