using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.Purposes.Commands;
using MFO.Application.ReferenceData.Purposes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/purposes")]
public sealed class PurposesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurposesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReferenceItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetPurposesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetPurposeByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceItemDto>> Create(ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreatePurposeCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> Update(Guid id, ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdatePurposeCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeletePurposeCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
