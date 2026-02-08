using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record CreatePenaltyPolicyCommand(PenaltyPolicyRequest Request) : IRequest<PenaltyPolicyDto>;

public sealed class CreatePenaltyPolicyCommandHandler : IRequestHandler<CreatePenaltyPolicyCommand, PenaltyPolicyDto>
{
    private readonly ICrudRepository<PenaltyPolicy> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePenaltyPolicyCommandHandler(ICrudRepository<PenaltyPolicy> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PenaltyPolicyDto> Handle(CreatePenaltyPolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = new PenaltyPolicy
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            PenaltyRate = request.Request.PenaltyRate,
            FixedFee = request.Request.FixedFee,
            IsActive = request.Request.IsActive
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new PenaltyPolicyDto(entity.Id, entity.Code, entity.Name, entity.PenaltyRate, entity.FixedFee, entity.IsActive);
    }
}
