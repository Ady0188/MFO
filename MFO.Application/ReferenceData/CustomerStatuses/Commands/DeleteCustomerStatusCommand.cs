using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerStatuses.Commands;

public sealed record DeleteCustomerStatusCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteCustomerStatusCommandHandler : IRequestHandler<DeleteCustomerStatusCommand, bool>
{
    private readonly ICrudRepository<CustomerStatus> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerStatusCommandHandler(ICrudRepository<CustomerStatus> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _repository.RemoveAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
