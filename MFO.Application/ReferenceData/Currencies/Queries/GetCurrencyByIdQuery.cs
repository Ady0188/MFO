using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Currencies.Queries;

public sealed record GetCurrencyByIdQuery(Guid Id) : IRequest<CurrencyDto?>;

public sealed class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetCurrencyByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CurrencyDto?> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new CurrencyDto(item.Id, item.Code, item.Name, item.Symbol, item.IsActive);
    }
}
