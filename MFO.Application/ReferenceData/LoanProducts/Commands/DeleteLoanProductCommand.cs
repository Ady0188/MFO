using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanProducts.Commands;

public sealed record DeleteLoanProductCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanProductCommandHandler : IRequestHandler<DeleteLoanProductCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteLoanProductCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteLoanProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.LoanProducts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.LoanProducts.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
