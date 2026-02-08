using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.PenaltyPolicies.Commands;
using MFO.Application.ReferenceData.PenaltyPolicies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/penalty-policies")]
public sealed class PenaltyPoliciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PenaltyPoliciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PenaltyPolicyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetPenaltyPoliciesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PenaltyPolicyDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetPenaltyPolicyByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<PenaltyPolicyDto>> Create(PenaltyPolicyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreatePenaltyPolicyCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PenaltyPolicyDto>> Update(Guid id, PenaltyPolicyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdatePenaltyPolicyCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeletePenaltyPolicyCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
