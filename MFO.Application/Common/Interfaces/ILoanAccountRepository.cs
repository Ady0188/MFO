using MFO.Domain.Entities;

namespace MFO.Application.Common.Interfaces;

public interface ILoanAccountRepository
{
    Task<IReadOnlyList<LoanAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task<LoanAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<LoanAccount?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(LoanAccount account, CancellationToken cancellationToken);
    Task RemoveAsync(LoanAccount account);
    Task<bool> AccountNumberExistsAsync(string accountNumber, Guid? excludeId, CancellationToken cancellationToken);
}
