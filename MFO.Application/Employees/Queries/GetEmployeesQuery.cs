using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.Employees.Queries;

public sealed record GetEmployeesQuery : IRequest<IReadOnlyList<EmployeeDto>>;

public sealed class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    private readonly ICrudRepository<Employee> _repository;

    public GetEmployeesQueryHandler(ICrudRepository<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new EmployeeDto(x.Id, x.FullName, x.BranchId, x.IsActive)).ToList();
    }
}
