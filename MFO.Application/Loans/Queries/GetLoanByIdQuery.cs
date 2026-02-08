using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Queries;

public sealed record GetLoanByIdQuery(Guid Id) : IRequest<LoanDto?>;

public sealed class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetLoanByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoanDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return loan is null ? null : LoanMappings.ToDto(loan);
    }
}
