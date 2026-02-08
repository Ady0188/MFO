using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Queries;

public sealed record GetPaymentFrequenciesQuery : IRequest<IReadOnlyList<PaymentFrequencyDto>>;

public sealed class GetPaymentFrequenciesQueryHandler : IRequestHandler<GetPaymentFrequenciesQuery, IReadOnlyList<PaymentFrequencyDto>>
{
    private readonly ICrudRepository<PaymentFrequency> _repository;

    public GetPaymentFrequenciesQueryHandler(ICrudRepository<PaymentFrequency> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PaymentFrequencyDto>> Handle(GetPaymentFrequenciesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new PaymentFrequencyDto(x.Id, x.Code, x.Name, x.IntervalDays, x.IsActive)).ToList();
    }
}
