using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record CreatePenaltyPolicyCommand(PenaltyPolicyRequest Request) : IRequest<PenaltyPolicyDto>;

public sealed class CreatePenaltyPolicyCommandHandler : IRequestHandler<CreatePenaltyPolicyCommand, PenaltyPolicyDto>
{
    private readonly IAppDbContext _dbContext;

    public CreatePenaltyPolicyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
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

        _dbContext.PenaltyPolicies.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PenaltyPolicyDto(entity.Id, entity.Code, entity.Name, entity.PenaltyRate, entity.FixedFee, entity.IsActive);
    }
}
