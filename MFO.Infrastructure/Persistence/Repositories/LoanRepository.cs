using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Infrastructure.Persistence.Repositories;

internal sealed class LoanRepository : ILoanRepository
{
    private readonly MfoDbContext _dbContext;

    public LoanRepository(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Loans.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Loans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Loan?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Loans.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken)
    {
        await _dbContext.Loans.AddAsync(loan, cancellationToken);
    }

    public Task RemoveAsync(Loan loan)
    {
        _dbContext.Loans.Remove(loan);
        return Task.CompletedTask;
    }

    public async Task<bool> LoanNumberExistsAsync(string loanNumber, Guid? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Loans.AsQueryable().Where(x => x.LoanNumber == loanNumber);
        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

}
