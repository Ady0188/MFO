namespace MFO.Domain.Entities;

public sealed class LoanProduct
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal InterestRate { get; set; }
    public decimal OriginationFee { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public int MinTermMonths { get; set; }
    public int MaxTermMonths { get; set; }
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public Guid PaymentFrequencyId { get; set; }
    public PaymentFrequency PaymentFrequency { get; set; } = null!;
    public Guid PenaltyPolicyId { get; set; }
    public PenaltyPolicy PenaltyPolicy { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
