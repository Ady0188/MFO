using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.Branches.Commands;
using MFO.Application.ReferenceData.Branches.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/branches")]
public sealed class BranchesController : BaseController
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReferenceItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetBranchesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetBranchByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceItemDto>> Create(ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreateBranchCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> Update(Guid id, ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdateBranchCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteBranchCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
