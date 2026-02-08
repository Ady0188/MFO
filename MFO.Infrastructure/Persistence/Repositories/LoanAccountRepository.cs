using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Infrastructure.Persistence.Repositories;

internal sealed class LoanAccountRepository : ILoanAccountRepository
{
    private readonly MfoDbContext _dbContext;

    public LoanAccountRepository(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<LoanAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.LoanAccounts.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<LoanAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanAccounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<LoanAccount?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(LoanAccount account, CancellationToken cancellationToken)
    {
        await _dbContext.LoanAccounts.AddAsync(account, cancellationToken);
    }

    public Task RemoveAsync(LoanAccount account)
    {
        _dbContext.LoanAccounts.Remove(account);
        return Task.CompletedTask;
    }

    public async Task<bool> AccountNumberExistsAsync(string accountNumber, Guid? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.LoanAccounts.AsQueryable().Where(x => x.AccountNumber == accountNumber);
        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}
