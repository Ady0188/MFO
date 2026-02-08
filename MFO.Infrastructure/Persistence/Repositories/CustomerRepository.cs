using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Infrastructure.Persistence.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly MfoDbContext _dbContext;

    public CustomerRepository(MfoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Customers.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
