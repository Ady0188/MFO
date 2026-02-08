using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record UpdateRepaymentMethodCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdateRepaymentMethodCommandHandler : IRequestHandler<UpdateRepaymentMethodCommand, ReferenceItemDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdateRepaymentMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto?> Handle(UpdateRepaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RepaymentMethods.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
