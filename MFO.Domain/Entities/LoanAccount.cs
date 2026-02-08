namespace MFO.Domain.Entities;

public sealed class LoanAccount
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public Loan Loan { get; set; } = null!;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateOnly OpenedOn { get; set; }
    public DateOnly? ClosedOn { get; set; }
    public bool IsActive { get; set; }
    public ICollection<LoanTransaction> Transactions { get; set; } = new List<LoanTransaction>();
}
