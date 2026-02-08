using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record CreateCurrencyCommand(CurrencyRequest Request) : IRequest<CurrencyDto>;

public sealed class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CurrencyDto>
{
    private readonly ICrudRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCurrencyCommandHandler(ICrudRepository<Currency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
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

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CurrencyDto(entity.Id, entity.Code, entity.Name, entity.Symbol, entity.IsActive);
    }
}
