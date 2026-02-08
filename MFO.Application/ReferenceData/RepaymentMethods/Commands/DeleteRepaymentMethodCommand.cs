using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record DeleteRepaymentMethodCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteRepaymentMethodCommandHandler : IRequestHandler<DeleteRepaymentMethodCommand, bool>
{
    private readonly ICrudRepository<RepaymentMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRepaymentMethodCommandHandler(ICrudRepository<RepaymentMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteRepaymentMethodCommand request, CancellationToken cancellationToken)
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
