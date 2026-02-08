using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.PaymentFrequencies.Commands;
using MFO.Application.ReferenceData.PaymentFrequencies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[ApiController]
[Route("api/payment-frequencies")]
public sealed class PaymentFrequenciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentFrequenciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaymentFrequencyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetPaymentFrequenciesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PaymentFrequencyDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetPaymentFrequencyByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentFrequencyDto>> Create(PaymentFrequencyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new CreatePaymentFrequencyCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PaymentFrequencyDto>> Update(Guid id, PaymentFrequencyRequest request, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new UpdatePaymentFrequencyCommand(id, request), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeletePaymentFrequencyCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
