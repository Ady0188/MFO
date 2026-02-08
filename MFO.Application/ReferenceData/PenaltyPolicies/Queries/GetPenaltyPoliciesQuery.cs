using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Queries;

public sealed record GetPenaltyPoliciesQuery : IRequest<IReadOnlyList<PenaltyPolicyDto>>;

public sealed class GetPenaltyPoliciesQueryHandler : IRequestHandler<GetPenaltyPoliciesQuery, IReadOnlyList<PenaltyPolicyDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetPenaltyPoliciesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<PenaltyPolicyDto>> Handle(GetPenaltyPoliciesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.PenaltyPolicies
            .AsNoTracking()
            .Select(x => new PenaltyPolicyDto(x.Id, x.Code, x.Name, x.PenaltyRate, x.FixedFee, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
