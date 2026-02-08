using MediatR;
using MFO.Application.LoanTransactions;
using MFO.Application.LoanTransactions.Commands;
using MFO.Application.LoanTransactions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/loan-transactions")]
public sealed class LoanTransactionsController : BaseController
{
    private readonly IMediator _mediator;

    public LoanTransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanTransactionDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoanTransactionsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanTransactionDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetLoanTransactionByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LoanTransactionDto>> Create(LoanTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateLoanTransactionCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPost("disbursements")]
    public async Task<ActionResult<LoanTransactionDto>> Disburse(LoanOperationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateDisbursementCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPost("repayments")]
    public async Task<ActionResult<LoanTransactionDto>> Repay(LoanOperationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateRepaymentCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPost("account-topups")]
    public async Task<ActionResult<LoanTransactionDto>> TopUpAccount(LoanOperationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateAccountTopUpCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanTransactionDto>> Update(Guid id, LoanTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateLoanTransactionCommand(id, request), cancellationToken);
        return FromCommandResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanTransactionCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
