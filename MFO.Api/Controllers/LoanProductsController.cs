using MediatR;
using MFO.Application.ReferenceData;
using MFO.Application.ReferenceData.LoanProducts.Commands;
using MFO.Application.ReferenceData.LoanProducts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/loan-products")]
public sealed class LoanProductsController : BaseController
{
    private readonly IMediator _mediator;

    public LoanProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetLoanProductsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetLoanProductByIdQuery(id), cancellationToken);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LoanProductDto>> Create(LoanProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateLoanProductCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoanProductDto>> Update(Guid id, LoanProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateLoanProductCommand(id, request), cancellationToken);
        return FromCommandResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteLoanProductCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

}
