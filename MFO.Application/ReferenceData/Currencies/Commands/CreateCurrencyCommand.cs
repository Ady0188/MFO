using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record CreateCurrencyCommand(CurrencyRequest Request) : IRequest<CurrencyDto>;

public sealed class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CurrencyDto>
{
    private readonly IAppDbContext _dbContext;

    public CreateCurrencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CurrencyDto> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Currency
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            Symbol = request.Request.Symbol,
            IsActive = request.Request.IsActive
        };

        _dbContext.Currencies.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CurrencyDto(entity.Id, entity.Code, entity.Name, entity.Symbol, entity.IsActive);
    }
}
