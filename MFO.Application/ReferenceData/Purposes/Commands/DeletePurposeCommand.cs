using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Purposes.Commands;

public sealed record DeletePurposeCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePurposeCommandHandler : IRequestHandler<DeletePurposeCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeletePurposeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeletePurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Purposes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Purposes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
