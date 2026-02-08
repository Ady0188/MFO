using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record UpdatePenaltyPolicyCommand(Guid Id, PenaltyPolicyRequest Request) : IRequest<PenaltyPolicyDto?>;

public sealed class UpdatePenaltyPolicyCommandHandler : IRequestHandler<UpdatePenaltyPolicyCommand, PenaltyPolicyDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdatePenaltyPolicyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PenaltyPolicyDto?> Handle(UpdatePenaltyPolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PenaltyPolicies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.PenaltyRate = request.Request.PenaltyRate;
        entity.FixedFee = request.Request.FixedFee;
        entity.IsActive = request.Request.IsActive;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PenaltyPolicyDto(entity.Id, entity.Code, entity.Name, entity.PenaltyRate, entity.FixedFee, entity.IsActive);
    }
}
