using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Queries;

public sealed record GetLoanProductByIdQuery(Guid Id) : IRequest<LoanProductDto?>;

public sealed class GetLoanProductByIdQueryHandler : IRequestHandler<GetLoanProductByIdQuery, LoanProductDto?>
{
    private readonly ICrudRepository<LoanProduct> _repository;

    public GetLoanProductByIdQueryHandler(ICrudRepository<LoanProduct> repository)
    {
        _repository = repository;
    }

    public async Task<LoanProductDto?> Handle(GetLoanProductByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

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
