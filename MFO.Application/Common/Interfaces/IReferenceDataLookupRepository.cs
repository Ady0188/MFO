namespace MFO.Application.Common.Interfaces;

public interface IReferenceDataLookupRepository
{
    Task<bool> LoanStatusExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> LoanProductExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> CurrencyExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DisbursementMethodExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> RepaymentMethodExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> PurposeExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> PaymentFrequencyExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> PenaltyPolicyExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> CustomerStatusExistsAsync(Guid id, CancellationToken cancellationToken);
}
