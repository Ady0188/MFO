using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanStatuses.Queries;

public sealed record GetLoanStatusByIdQuery(Guid Id) : IRequest<LoanStatusDto?>;

public sealed class GetLoanStatusByIdQueryHandler : IRequestHandler<GetLoanStatusByIdQuery, LoanStatusDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetLoanStatusByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoanStatusDto?> Handle(GetLoanStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.LoanStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new LoanStatusDto(item.Id, item.Code, item.Name, item.IsClosed);
    }
}
