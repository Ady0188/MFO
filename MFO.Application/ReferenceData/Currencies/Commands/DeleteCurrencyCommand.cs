using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.Currencies.Commands;

public sealed record DeleteCurrencyCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteCurrencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Currencies.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
