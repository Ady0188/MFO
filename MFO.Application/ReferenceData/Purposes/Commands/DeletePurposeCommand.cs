using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Purposes.Commands;

public sealed record DeletePurposeCommand(Guid Id) : IRequest<bool>;

public sealed class DeletePurposeCommandHandler : IRequestHandler<DeletePurposeCommand, bool>
{
    private readonly ICrudRepository<Purpose> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePurposeCommandHandler(ICrudRepository<Purpose> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePurposeCommand request, CancellationToken cancellationToken)
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
