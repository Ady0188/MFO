namespace MFO.Domain.Entities;

public sealed class LoanTransaction
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public Loan Loan { get; set; } = null!;
    public Guid LoanAccountId { get; set; }
    public LoanAccount LoanAccount { get; set; } = null!;
    public Guid TransactionTypeId { get; set; }
    public LoanTransactionType TransactionType { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateOnly OccurredOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
