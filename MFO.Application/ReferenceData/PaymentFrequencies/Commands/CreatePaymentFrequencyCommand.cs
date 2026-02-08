using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record CreatePaymentFrequencyCommand(PaymentFrequencyRequest Request) : IRequest<PaymentFrequencyDto>;

public sealed class CreatePaymentFrequencyCommandHandler : IRequestHandler<CreatePaymentFrequencyCommand, PaymentFrequencyDto>
{
    private readonly IAppDbContext _dbContext;

    public CreatePaymentFrequencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
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

        _dbContext.PaymentFrequencies.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PaymentFrequencyDto(entity.Id, entity.Code, entity.Name, entity.IntervalDays, entity.IsActive);
    }
}
