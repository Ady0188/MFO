using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record CreatePaymentFrequencyCommand(PaymentFrequencyRequest Request) : IRequest<PaymentFrequencyDto>;

public sealed class CreatePaymentFrequencyCommandHandler : IRequestHandler<CreatePaymentFrequencyCommand, PaymentFrequencyDto>
{
    private readonly ICrudRepository<PaymentFrequency> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentFrequencyCommandHandler(ICrudRepository<PaymentFrequency> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentFrequencyDto> Handle(CreatePaymentFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = new PaymentFrequency
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IntervalDays = request.Request.IntervalDays,
            IsActive = request.Request.IsActive
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new PaymentFrequencyDto(entity.Id, entity.Code, entity.Name, entity.IntervalDays, entity.IsActive);
    }
}
