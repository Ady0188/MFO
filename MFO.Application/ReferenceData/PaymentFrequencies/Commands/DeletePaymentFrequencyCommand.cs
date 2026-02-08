using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record DeletePaymentFrequencyCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePaymentFrequencyCommandHandler : IRequestHandler<DeletePaymentFrequencyCommand, bool>
{
    private readonly ICrudRepository<PaymentFrequency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentFrequencyCommandHandler(ICrudRepository<PaymentFrequency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePaymentFrequencyCommand request, CancellationToken cancellationToken)
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
