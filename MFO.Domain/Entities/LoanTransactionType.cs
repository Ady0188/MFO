namespace MFO.Domain.Entities;

public sealed class LoanTransactionType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<LoanTransaction> Transactions { get; set; } = new List<LoanTransaction>();
}
