using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanStatuses.Queries;

public sealed record GetLoanStatusesQuery : IRequest<IReadOnlyList<LoanStatusDto>>;

public sealed class GetLoanStatusesQueryHandler : IRequestHandler<GetLoanStatusesQuery, IReadOnlyList<LoanStatusDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetLoanStatusesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<LoanStatusDto>> Handle(GetLoanStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanStatuses
            .AsNoTracking()
            .Select(x => new LoanStatusDto(x.Id, x.Code, x.Name, x.IsClosed))
            .ToListAsync(cancellationToken);
    }
}
