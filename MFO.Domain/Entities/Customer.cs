using MFO.Domain.Common;

namespace MFO.Domain.Entities;

public sealed class Customer : IAggregateRoot
{
    private Customer()
    {
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string NationalId { get; private set; } = string.Empty;
    public DateOnly DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public Guid StatusId { get; private set; }
    public CustomerStatus Status { get; private set; } = null!;
    public ICollection<Loan> Loans { get; private set; } = new List<Loan>();

    public static Customer Create(
        string fullName,
        string nationalId,
        DateOnly dateOfBirth,
        string phoneNumber,
        Guid statusId)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            NationalId = nationalId,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber,
            StatusId = statusId
        };
    }

    public void Update(
        string fullName,
        string nationalId,
        DateOnly dateOfBirth,
        string phoneNumber,
        Guid statusId)
    {
        FullName = fullName;
        NationalId = nationalId;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        StatusId = statusId;
    }
}
