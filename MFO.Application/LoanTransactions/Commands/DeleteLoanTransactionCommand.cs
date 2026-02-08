using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Commands;

public sealed record DeleteLoanTransactionCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanTransactionCommandHandler : IRequestHandler<DeleteLoanTransactionCommand, bool>
{
    private readonly ICrudRepository<LoanTransaction> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanTransactionCommandHandler(ICrudRepository<LoanTransaction> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _repository.RemoveAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
