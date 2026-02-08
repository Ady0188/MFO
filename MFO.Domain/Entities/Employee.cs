namespace MFO.Domain.Entities;

public sealed class Employee
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<Loan> CuratedLoans { get; set; } = new List<Loan>();
}
