using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.RepaymentMethods.Queries;

public sealed record GetRepaymentMethodByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetRepaymentMethodByIdQueryHandler : IRequestHandler<GetRepaymentMethodByIdQuery, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetRepaymentMethodByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(GetRepaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.RepaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
