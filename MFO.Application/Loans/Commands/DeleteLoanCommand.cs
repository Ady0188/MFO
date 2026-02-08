using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Commands;

public sealed record DeleteLoanCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteLoanCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (loan is null)
        {
            return false;
        }

        _dbContext.Loans.Remove(loan);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
