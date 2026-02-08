using MFO.Domain.Entities;

namespace MFO.Application.Common.Interfaces;

public interface ILoanRepository
{
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken);
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Loan?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Loan loan, CancellationToken cancellationToken);
    Task RemoveAsync(Loan loan);
    Task<bool> LoanNumberExistsAsync(string loanNumber, Guid? excludeId, CancellationToken cancellationToken);
}
