using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.DisbursementMethods.Queries;

public sealed record GetDisbursementMethodsQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetDisbursementMethodsQueryHandler : IRequestHandler<GetDisbursementMethodsQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetDisbursementMethodsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetDisbursementMethodsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.DisbursementMethods
            .AsNoTracking()
            .Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
