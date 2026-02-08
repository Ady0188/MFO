using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Purposes.Queries;

public sealed record GetPurposesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetPurposesQueryHandler : IRequestHandler<GetPurposesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetPurposesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetPurposesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Purposes
            .AsNoTracking()
            .Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
