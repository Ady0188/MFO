using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Commands;

public sealed record DeletePenaltyPolicyCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePenaltyPolicyCommandHandler : IRequestHandler<DeletePenaltyPolicyCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeletePenaltyPolicyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeletePenaltyPolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PenaltyPolicies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PenaltyPolicies.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
