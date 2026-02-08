using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.DisbursementMethods.Commands;
using MFO.Application.ReferenceData.DisbursementMethods.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/disbursement-methods")]
public sealed class DisbursementMethodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisbursementMethodsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReferenceItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetDisbursementMethodsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetDisbursementMethodByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceItemDto>> Create(ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreateDisbursementMethodCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> Update(Guid id, ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdateDisbursementMethodCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteDisbursementMethodCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
