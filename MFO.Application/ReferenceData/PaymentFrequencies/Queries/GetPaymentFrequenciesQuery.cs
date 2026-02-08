using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Queries;

public sealed record GetPaymentFrequenciesQuery : IRequest<IReadOnlyList<PaymentFrequencyDto>>;

public sealed class GetPaymentFrequenciesQueryHandler : IRequestHandler<GetPaymentFrequenciesQuery, IReadOnlyList<PaymentFrequencyDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetPaymentFrequenciesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<PaymentFrequencyDto>> Handle(GetPaymentFrequenciesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.PaymentFrequencies
            .AsNoTracking()
            .Select(x => new PaymentFrequencyDto(x.Id, x.Code, x.Name, x.IntervalDays, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
