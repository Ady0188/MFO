using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record DeleteLoanStatusCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanStatusCommandHandler : IRequestHandler<DeleteLoanStatusCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteLoanStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteLoanStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.LoanStatuses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.LoanStatuses.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
