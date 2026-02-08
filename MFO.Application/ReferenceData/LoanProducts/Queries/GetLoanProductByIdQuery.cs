using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanProducts.Queries;

public sealed record GetLoanProductByIdQuery(Guid Id) : IRequest<LoanProductDto?>;

public sealed class GetLoanProductByIdQueryHandler : IRequestHandler<GetLoanProductByIdQuery, LoanProductDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetLoanProductByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoanProductDto?> Handle(GetLoanProductByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.LoanProducts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null
            ? null
            : new LoanProductDto(
                item.Id,
                item.Code,
                item.Name,
                item.InterestRate,
                item.OriginationFee,
                item.MinAmount,
                item.MaxAmount,
                item.MinTermMonths,
                item.MaxTermMonths,
                item.CurrencyId,
                item.PaymentFrequencyId,
                item.PenaltyPolicyId,
                item.IsActive);
    }
}
