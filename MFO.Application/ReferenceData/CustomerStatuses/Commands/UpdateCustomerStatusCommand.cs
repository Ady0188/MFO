using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.CustomerStatuses.Commands;

public sealed record UpdateCustomerStatusCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdateCustomerStatusCommandHandler : IRequestHandler<UpdateCustomerStatusCommand, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdateCustomerStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.CustomerStatuses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IsActive = request.Request.IsActive;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
