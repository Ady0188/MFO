using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.CustomerStatuses.Queries;

public sealed record GetCustomerStatusByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetCustomerStatusByIdQueryHandler : IRequestHandler<GetCustomerStatusByIdQuery, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetCustomerStatusByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(GetCustomerStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.CustomerStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
