using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record UpdateCurrencyCommand(Guid Id, CurrencyRequest Request) : IRequest<CurrencyDto?>;

public sealed class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, CurrencyDto?>
{
    private readonly ICrudRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCurrencyCommandHandler(ICrudRepository<Currency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CurrencyDto?> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.NumericCode = request.Request.NumericCode;
        entity.Name = request.Request.Name;
        entity.Symbol = request.Request.Symbol;
        entity.IsActive = request.Request.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CurrencyDto(entity.Id, entity.Code, entity.NumericCode, entity.Name, entity.Symbol, entity.IsActive);
    }
}
