using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.CustomerStatuses.Commands;

public sealed record DeleteCustomerStatusCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteCustomerStatusCommandHandler : IRequestHandler<DeleteCustomerStatusCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteCustomerStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.CustomerStatuses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.CustomerStatuses.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
