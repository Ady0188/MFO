using MediatR;
using MFO.Application.LoanAccounts;
using MFO.Application.LoanAccounts.Commands;
using MFO.Application.LoanAccounts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/loan-accounts")]
public sealed class LoanAccountsController : BaseController
{
    private readonly IMediator _mediator;

    public LoanAccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanAccountDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoanAccountsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanAccountDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetLoanAccountByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LoanAccountDto>> Create(LoanAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateLoanAccountCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanAccountDto>> Update(Guid id, LoanAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateLoanAccountCommand(id, request), cancellationToken);
        return FromCommandResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanAccountCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
