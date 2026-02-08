using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Infrastructure.Persistence.Repositories;

internal sealed class ReferenceDataLookupRepository : IReferenceDataLookupRepository
{
    private readonly MfoDbContext _dbContext;

    public ReferenceDataLookupRepository(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> LoanStatusExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.LoanStatuses.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> LoanProductExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.LoanProducts.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> CurrencyExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Currencies.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> BranchExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Branches.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> EmployeeExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Employees.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> DisbursementMethodExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.DisbursementMethods.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> RepaymentMethodExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.RepaymentMethods.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> PurposeExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Purposes.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> PaymentFrequencyExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.PaymentFrequencies.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> PenaltyPolicyExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.PenaltyPolicies.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> CustomerStatusExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.CustomerStatuses.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> CustomerTypeExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.CustomerTypes.AnyAsync(x => x.Id == id, cancellationToken);

    public Task<bool> LoanTransactionTypeExistsAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.LoanTransactionTypes.AnyAsync(x => x.Id == id, cancellationToken);

    public async Task<Guid?> GetLoanStatusIdByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanStatuses
            .Where(x => x.Code == code)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid?> GetLoanTransactionTypeIdByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await _dbContext.LoanTransactionTypes
            .Where(x => x.Code == code)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
