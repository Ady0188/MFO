using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Queries;

public sealed record GetPenaltyPolicyByIdQuery(Guid Id) : IRequest<PenaltyPolicyDto?>;

public sealed class GetPenaltyPolicyByIdQueryHandler : IRequestHandler<GetPenaltyPolicyByIdQuery, PenaltyPolicyDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetPenaltyPolicyByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PenaltyPolicyDto?> Handle(GetPenaltyPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.PenaltyPolicies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new PenaltyPolicyDto(item.Id, item.Code, item.Name, item.PenaltyRate, item.FixedFee, item.IsActive);
    }
}
