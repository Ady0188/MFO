using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record UpdateDisbursementMethodCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdateDisbursementMethodCommandHandler : IRequestHandler<UpdateDisbursementMethodCommand, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdateDisbursementMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(UpdateDisbursementMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.DisbursementMethods.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
