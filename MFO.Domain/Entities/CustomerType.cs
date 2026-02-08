namespace MFO.Domain.Entities;

public sealed class CustomerType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
