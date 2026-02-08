using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Queries;

public sealed record GetPaymentFrequencyByIdQuery(Guid Id) : IRequest<PaymentFrequencyDto?>;

public sealed class GetPaymentFrequencyByIdQueryHandler : IRequestHandler<GetPaymentFrequencyByIdQuery, PaymentFrequencyDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetPaymentFrequencyByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentFrequencyDto?> Handle(GetPaymentFrequencyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.PaymentFrequencies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new PaymentFrequencyDto(item.Id, item.Code, item.Name, item.IntervalDays, item.IsActive);
    }
}
