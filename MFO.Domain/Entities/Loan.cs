namespace MFO.Domain.Entities;

public sealed class Loan
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public DateOnly IssuedOn { get; set; }
    public DateOnly? ClosedOn { get; set; }
}