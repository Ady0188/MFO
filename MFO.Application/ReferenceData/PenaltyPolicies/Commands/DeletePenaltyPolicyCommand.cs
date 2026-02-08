using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record DeletePenaltyPolicyCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePenaltyPolicyCommandHandler : IRequestHandler<DeletePenaltyPolicyCommand, bool>
{
    private readonly ICrudRepository<PenaltyPolicy> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePenaltyPolicyCommandHandler(ICrudRepository<PenaltyPolicy> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePenaltyPolicyCommand request, CancellationToken cancellationToken)
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
