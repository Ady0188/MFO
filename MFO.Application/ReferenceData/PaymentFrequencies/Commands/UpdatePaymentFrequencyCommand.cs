using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record UpdatePaymentFrequencyCommand(Guid Id, PaymentFrequencyRequest Request) : IRequest<PaymentFrequencyDto?>;

public sealed class UpdatePaymentFrequencyCommandHandler : IRequestHandler<UpdatePaymentFrequencyCommand, PaymentFrequencyDto?>
{
    private readonly ICrudRepository<PaymentFrequency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentFrequencyCommandHandler(ICrudRepository<PaymentFrequency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentFrequencyDto?> Handle(UpdatePaymentFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IntervalDays = request.Request.IntervalDays;
        entity.IsActive = request.Request.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new PaymentFrequencyDto(entity.Id, entity.Code, entity.Name, entity.IntervalDays, entity.IsActive);
    }
}
