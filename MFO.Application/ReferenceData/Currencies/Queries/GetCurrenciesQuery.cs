using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Currencies.Queries;

public sealed record GetCurrenciesQuery : IRequest<IReadOnlyList<CurrencyDto>>;

public sealed class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetCurrenciesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies
            .AsNoTracking()
            .Select(x => new CurrencyDto(x.Id, x.Code, x.Name, x.Symbol, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
