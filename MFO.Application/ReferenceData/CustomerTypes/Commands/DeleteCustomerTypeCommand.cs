using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerTypes.Commands;

public sealed record DeleteCustomerTypeCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteCustomerTypeCommandHandler : IRequestHandler<DeleteCustomerTypeCommand, bool>
{
    private readonly ICrudRepository<CustomerType> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerTypeCommandHandler(ICrudRepository<CustomerType> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCustomerTypeCommand request, CancellationToken cancellationToken)
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
