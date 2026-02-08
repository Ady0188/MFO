using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.Employees.Commands;

public sealed record CreateEmployeeCommand(EmployeeRequest Request) : IRequest<CommandResult<EmployeeDto>>;

public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CommandResult<EmployeeDto>>
{
    private readonly ICrudRepository<Employee> _repository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(
        ICrudRepository<Employee> repository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await _referenceLookup.BranchExistsAsync(request.Request.BranchId, cancellationToken))
        {
            return CommandResult<EmployeeDto>.Missing(new[] { "Branch" });
        }

        var entity = new Employee
        {
            Id = Guid.NewGuid(),
            FullName = request.Request.FullName,
            BranchId = request.Request.BranchId,
            IsActive = request.Request.IsActive
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<EmployeeDto>.Success(new EmployeeDto(entity.Id, entity.FullName, entity.BranchId, entity.IsActive));
    }
}
