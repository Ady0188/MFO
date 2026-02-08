namespace MFO.Domain.Entities;

public sealed class Customer
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}