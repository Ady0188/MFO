using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.DisbursementMethods.Queries;

public sealed record GetDisbursementMethodByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetDisbursementMethodByIdQueryHandler : IRequestHandler<GetDisbursementMethodByIdQuery, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetDisbursementMethodByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(GetDisbursementMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.DisbursementMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
