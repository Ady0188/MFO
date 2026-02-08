using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Queries;

public sealed record GetLoanProductsQuery : IRequest<IReadOnlyList<LoanProductDto>>;

public sealed class GetLoanProductsQueryHandler : IRequestHandler<GetLoanProductsQuery, IReadOnlyList<LoanProductDto>>
{
    private readonly ICrudRepository<LoanProduct> _repository;

    public GetLoanProductsQueryHandler(ICrudRepository<LoanProduct> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LoanProductDto>> Handle(GetLoanProductsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new LoanProductDto(
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
            x.IsActive)).ToList();
    }
}
