using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record DeleteDisbursementMethodCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteDisbursementMethodCommandHandler : IRequestHandler<DeleteDisbursementMethodCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteDisbursementMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteDisbursementMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.DisbursementMethods.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.DisbursementMethods.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
