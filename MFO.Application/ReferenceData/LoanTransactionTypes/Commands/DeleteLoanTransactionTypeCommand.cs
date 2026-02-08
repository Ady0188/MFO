using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanTransactionTypes.Commands;

public sealed record DeleteLoanTransactionTypeCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanTransactionTypeCommandHandler : IRequestHandler<DeleteLoanTransactionTypeCommand, bool>
{
    private readonly ICrudRepository<LoanTransactionType> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanTransactionTypeCommandHandler(ICrudRepository<LoanTransactionType> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanTransactionTypeCommand request, CancellationToken cancellationToken)
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
