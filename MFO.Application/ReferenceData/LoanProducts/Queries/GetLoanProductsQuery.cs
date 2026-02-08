using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanProducts.Queries;

public sealed record GetLoanProductsQuery : IRequest<IReadOnlyList<LoanProductDto>>;

public sealed class GetLoanProductsQueryHandler : IRequestHandler<GetLoanProductsQuery, IReadOnlyList<LoanProductDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetLoanProductsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<LoanProductDto>> Handle(GetLoanProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanProducts
            .AsNoTracking()
            .Select(x => new LoanProductDto(
                x.Id,
                x.Code,
                x.Name,
                x.InterestRate,
                x.OriginationFee,
                x.MinAmount,
                x.MaxAmount,
                x.MinTermMonths,
                x.MaxTermMonths,
                x.CurrencyId,
                x.PaymentFrequencyId,
                x.PenaltyPolicyId,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
