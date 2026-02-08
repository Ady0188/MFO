using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.Currencies.Commands;
using MFO.Application.ReferenceData.Currencies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/currencies")]
public sealed class CurrenciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrenciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CurrencyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetCurrenciesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CurrencyDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetCurrencyByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CurrencyDto>> Create(CurrencyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreateCurrencyCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CurrencyDto>> Update(Guid id, CurrencyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdateCurrencyCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteCurrencyCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
