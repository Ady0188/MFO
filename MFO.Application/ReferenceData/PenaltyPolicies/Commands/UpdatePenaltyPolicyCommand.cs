using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record UpdatePenaltyPolicyCommand(Guid Id, PenaltyPolicyRequest Request) : IRequest<PenaltyPolicyDto?>;

public sealed class UpdatePenaltyPolicyCommandHandler : IRequestHandler<UpdatePenaltyPolicyCommand, PenaltyPolicyDto?>
{
    private readonly ICrudRepository<PenaltyPolicy> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePenaltyPolicyCommandHandler(ICrudRepository<PenaltyPolicy> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PenaltyPolicyDto?> Handle(UpdatePenaltyPolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.PenaltyRate = request.Request.PenaltyRate;
        entity.FixedFee = request.Request.FixedFee;
        entity.IsActive = request.Request.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new PenaltyPolicyDto(entity.Id, entity.Code, entity.Name, entity.PenaltyRate, entity.FixedFee, entity.IsActive);
    }
}
