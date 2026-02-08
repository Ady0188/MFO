namespace MFO.Domain.Entities;

public sealed class DisbursementMethod
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
