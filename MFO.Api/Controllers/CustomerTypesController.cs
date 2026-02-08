using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.CustomerTypes.Commands;
using MFO.Application.ReferenceData.CustomerTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/customer-types")]
public sealed class CustomerTypesController : BaseController
{
    private readonly IMediator _mediator;

    public CustomerTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReferenceItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetCustomerTypesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetCustomerTypeByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceItemDto>> Create(ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreateCustomerTypeCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReferenceItemDto>> Update(Guid id, ReferenceItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdateCustomerTypeCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteCustomerTypeCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
