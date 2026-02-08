using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.Employees.Commands;

public sealed record UpdateEmployeeCommand(Guid Id, EmployeeRequest Request) : IRequest<CommandResult<EmployeeDto>>;

public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, CommandResult<EmployeeDto>>
{
    private readonly ICrudRepository<Employee> _repository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(
        ICrudRepository<Employee> repository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return CommandResult<EmployeeDto>.NotFound();
        }

        if (!await _referenceLookup.BranchExistsAsync(request.Request.BranchId, cancellationToken))
        {
            return CommandResult<EmployeeDto>.Missing(new[] { "Branch" });
        }

        entity.FullName = request.Request.FullName;
        entity.BranchId = request.Request.BranchId;
        entity.IsActive = request.Request.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<EmployeeDto>.Success(new EmployeeDto(entity.Id, entity.FullName, entity.BranchId, entity.IsActive));
    }
}
