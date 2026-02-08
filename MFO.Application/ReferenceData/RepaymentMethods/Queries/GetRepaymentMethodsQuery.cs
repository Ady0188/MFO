using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.RepaymentMethods.Queries;

public sealed record GetRepaymentMethodsQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetRepaymentMethodsQueryHandler : IRequestHandler<GetRepaymentMethodsQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetRepaymentMethodsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetRepaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.RepaymentMethods
            .AsNoTracking()
            .Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
