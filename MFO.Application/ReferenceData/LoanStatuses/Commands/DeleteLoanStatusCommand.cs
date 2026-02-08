using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record DeleteLoanStatusCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanStatusCommandHandler : IRequestHandler<DeleteLoanStatusCommand, bool>
{
    private readonly ICrudRepository<LoanStatus> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanStatusCommandHandler(ICrudRepository<LoanStatus> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanStatusCommand request, CancellationToken cancellationToken)
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
