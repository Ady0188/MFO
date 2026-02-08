using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Queries;

public sealed record GetCurrenciesQuery : IRequest<IReadOnlyList<CurrencyDto>>;

public sealed class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>>
{
    private readonly ICrudRepository<Currency> _repository;

    public GetCurrenciesQueryHandler(ICrudRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new CurrencyDto(x.Id, x.Code, x.Name, x.Symbol, x.IsActive)).ToList();
    }
}
