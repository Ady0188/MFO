using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.CustomerStatuses.Queries;

public sealed record GetCustomerStatusesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetCustomerStatusesQueryHandler : IRequestHandler<GetCustomerStatusesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetCustomerStatusesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetCustomerStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.CustomerStatuses
            .AsNoTracking()
            .Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
