using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Queries;

public sealed record GetPaymentFrequencyByIdQuery(Guid Id) : IRequest<PaymentFrequencyDto?>;

public sealed class GetPaymentFrequencyByIdQueryHandler : IRequestHandler<GetPaymentFrequencyByIdQuery, PaymentFrequencyDto?>
{
    private readonly ICrudRepository<PaymentFrequency> _repository;

    public GetPaymentFrequencyByIdQueryHandler(ICrudRepository<PaymentFrequency> repository)
    {
        _repository = repository;
    }

    public async Task<PaymentFrequencyDto?> Handle(GetPaymentFrequencyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new PaymentFrequencyDto(item.Id, item.Code, item.Name, item.IntervalDays, item.IsActive);
    }
}
