using MediatR;
using MFO.Application.Employees;
using MFO.Application.Employees.Commands;
using MFO.Application.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MFO.Api.Controllers;

[Route("api/employees")]
public sealed class EmployeesController : BaseController
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new GetEmployeesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetEmployeeByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(EmployeeRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateEmployeeCommand(request), cancellationToken);
        if (result.Value is null)
        {
            return FromCommandResult(result);
        }

        return FromCommandResult(result, nameof(GetById), new { id = result.Value.Id });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> Update(Guid id, EmployeeRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateEmployeeCommand(id, request), cancellationToken);
        return FromCommandResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
