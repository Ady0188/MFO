using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Queries;

public sealed record GetLoansQuery : IRequest<IReadOnlyList<LoanDto>>;

public sealed class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, IReadOnlyList<LoanDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetLoansQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Loans
            .AsNoTracking()
            .Select(loan => new LoanDto(
                loan.Id,
                loan.LoanNumber,
                loan.CustomerId,
                loan.ProductId,
                loan.StatusId,
                loan.CurrencyId,
                loan.DisbursementMethodId,
                loan.RepaymentMethodId,
                loan.PurposeId,
                loan.PaymentFrequencyId,
                loan.PenaltyPolicyId,
                loan.PrincipalAmount,
                loan.InterestRate,
                loan.FeesAmount,
                loan.PenaltyRate,
                loan.TotalPayable,
                loan.OutstandingPrincipal,
                loan.OutstandingInterest,
                loan.TermMonths,
                loan.IssuedOn,
                loan.ApprovedOn,
                loan.DisbursedOn,
                loan.MaturityOn,
                loan.ClosedOn,
                loan.CreatedAt,
                loan.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
