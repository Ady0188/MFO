using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.LoanStatuses.Commands;
using MFO.Application.ReferenceData.LoanStatuses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/loan-statuses")]
public sealed class LoanStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoanStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanStatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoanStatusesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanStatusDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetLoanStatusByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LoanStatusDto>> Create(LoanStatusRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreateLoanStatusCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanStatusDto>> Update(Guid id, LoanStatusRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdateLoanStatusCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanStatusCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
