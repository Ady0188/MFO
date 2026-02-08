namespace MFO.Domain.Entities;

public sealed class PenaltyPolicy
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal PenaltyRate { get; set; }
    public decimal FixedFee { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<LoanProduct> LoanProducts { get; set; } = new List<LoanProduct>();
}
