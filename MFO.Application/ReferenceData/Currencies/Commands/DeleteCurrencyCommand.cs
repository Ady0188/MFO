using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record DeleteCurrencyCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, bool>
{
    private readonly ICrudRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCurrencyCommandHandler(ICrudRepository<Currency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _repository.RemoveAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
