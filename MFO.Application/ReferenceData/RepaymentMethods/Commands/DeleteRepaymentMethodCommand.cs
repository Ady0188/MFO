using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record DeleteRepaymentMethodCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteRepaymentMethodCommandHandler : IRequestHandler<DeleteRepaymentMethodCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeleteRepaymentMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteRepaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RepaymentMethods.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.RepaymentMethods.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
