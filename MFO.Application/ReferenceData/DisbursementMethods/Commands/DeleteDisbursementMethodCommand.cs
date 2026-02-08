using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record DeleteDisbursementMethodCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteDisbursementMethodCommandHandler : IRequestHandler<DeleteDisbursementMethodCommand, bool>
{
    private readonly ICrudRepository<DisbursementMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDisbursementMethodCommandHandler(ICrudRepository<DisbursementMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteDisbursementMethodCommand request, CancellationToken cancellationToken)
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
