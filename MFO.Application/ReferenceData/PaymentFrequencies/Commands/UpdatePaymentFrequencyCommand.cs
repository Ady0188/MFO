using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record UpdatePaymentFrequencyCommand(Guid Id, PaymentFrequencyRequest Request) : IRequest<PaymentFrequencyDto?>;

public sealed class UpdatePaymentFrequencyCommandHandler : IRequestHandler<UpdatePaymentFrequencyCommand, PaymentFrequencyDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdatePaymentFrequencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentFrequencyDto?> Handle(UpdatePaymentFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PaymentFrequencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IntervalDays = request.Request.IntervalDays;
        entity.IsActive = request.Request.IsActive;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PaymentFrequencyDto(entity.Id, entity.Code, entity.Name, entity.IntervalDays, entity.IsActive);
    }
}
