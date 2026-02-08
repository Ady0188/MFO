using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.PaymentFrequencies.Commands;

public sealed record DeletePaymentFrequencyCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePaymentFrequencyCommandHandler : IRequestHandler<DeletePaymentFrequencyCommand, bool>
{
    private readonly IAppDbContext _dbContext;

    public DeletePaymentFrequencyCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeletePaymentFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PaymentFrequencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbContext.PaymentFrequencies.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
