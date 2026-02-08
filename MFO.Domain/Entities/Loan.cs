namespace MFO.Domain.Entities;

public sealed class Loan
{
    public Guid Id { get; set; }
    public string LoanNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Guid ProductId { get; set; }
    public LoanProduct Product { get; set; } = null!;
    public Guid StatusId { get; set; }
    public LoanStatus Status { get; set; } = null!;
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public Guid DisbursementMethodId { get; set; }
    public DisbursementMethod DisbursementMethod { get; set; } = null!;
    public Guid RepaymentMethodId { get; set; }
    public RepaymentMethod RepaymentMethod { get; set; } = null!;
    public Guid PurposeId { get; set; }
    public Purpose Purpose { get; set; } = null!;
    public Guid PaymentFrequencyId { get; set; }
    public PaymentFrequency PaymentFrequency { get; set; } = null!;
    public Guid PenaltyPolicyId { get; set; }
    public PenaltyPolicy PenaltyPolicy { get; set; } = null!;
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public decimal FeesAmount { get; set; }
    public decimal PenaltyRate { get; set; }
    public decimal TotalPayable { get; set; }
    public decimal OutstandingPrincipal { get; set; }
    public decimal OutstandingInterest { get; set; }
    public int TermMonths { get; set; }
    public DateOnly IssuedOn { get; set; }
    public DateOnly? ApprovedOn { get; set; }
    public DateOnly? DisbursedOn { get; set; }
    public DateOnly? MaturityOn { get; set; }
    public DateOnly? ClosedOn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
