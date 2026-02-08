using MFO.Application.Common.Interfaces;

namespace MFO.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly MfoDbContext _dbContext;

    public UnitOfWork(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
