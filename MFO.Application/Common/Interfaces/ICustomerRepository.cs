namespace MFO.Application.Common.Interfaces;

public interface ICustomerRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}
