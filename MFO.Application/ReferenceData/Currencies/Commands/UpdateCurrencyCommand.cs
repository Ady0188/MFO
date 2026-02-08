using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record UpdateCurrencyCommand(Guid Id, CurrencyRequest Request) : IRequest<CurrencyDto?>;

public sealed class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, CurrencyDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdateCurrencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CurrencyDto?> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.Symbol = request.Request.Symbol;
        entity.IsActive = request.Request.IsActive;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CurrencyDto(entity.Id, entity.Code, entity.Name, entity.Symbol, entity.IsActive);
    }
}
