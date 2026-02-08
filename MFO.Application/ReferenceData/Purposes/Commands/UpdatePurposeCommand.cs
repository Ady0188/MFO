using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Purposes.Commands;

public sealed record UpdatePurposeCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdatePurposeCommandHandler : IRequestHandler<UpdatePurposeCommand, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdatePurposeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(UpdatePurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Purposes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
