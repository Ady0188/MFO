using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Purposes.Queries;

public sealed record GetPurposeByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetPurposeByIdQueryHandler : IRequestHandler<GetPurposeByIdQuery, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public GetPurposeByIdQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(GetPurposeByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Purposes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
