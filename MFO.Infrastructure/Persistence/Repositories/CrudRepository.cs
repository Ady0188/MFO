using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Infrastructure.Persistence.Repositories;

internal sealed class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly MfoDbContext _dbContext;
    private readonly DbSet<TEntity> _set;

    public CrudRepository(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<TEntity>();
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _set.FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _set.AsNoTracking()
            .FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _set.AddAsync(entity, cancellationToken);
    }

    public Task RemoveAsync(TEntity entity)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }

}
