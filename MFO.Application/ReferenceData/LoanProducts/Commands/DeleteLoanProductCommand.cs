using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Commands;

public sealed record DeleteLoanProductCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanProductCommandHandler : IRequestHandler<DeleteLoanProductCommand, bool>
{
    private readonly ICrudRepository<LoanProduct> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanProductCommandHandler(ICrudRepository<LoanProduct> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanProductCommand request, CancellationToken cancellationToken)
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
