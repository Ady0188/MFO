using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.Employees.Queries;

public sealed record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;

public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly ICrudRepository<Employee> _repository;

    public GetEmployeeByIdQueryHandler(ICrudRepository<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null ? null : new EmployeeDto(item.Id, item.FullName, item.BranchId, item.IsActive);
    }
}
