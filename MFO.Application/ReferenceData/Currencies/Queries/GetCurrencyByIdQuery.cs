using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Queries;

public sealed record GetCurrencyByIdQuery(Guid Id) : IRequest<CurrencyDto?>;

public sealed class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyDto?>
{
    private readonly ICrudRepository<Currency> _repository;

    public GetCurrencyByIdQueryHandler(ICrudRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyDto?> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new CurrencyDto(item.Id, item.Code, item.NumericCode, item.Name, item.Symbol, item.IsActive);
    }
}
