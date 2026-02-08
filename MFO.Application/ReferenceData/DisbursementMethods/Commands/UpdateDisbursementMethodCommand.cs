using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record UpdateDisbursementMethodCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdateDisbursementMethodCommandHandler : IRequestHandler<UpdateDisbursementMethodCommand, ReferenceItemDto?>
{
    private readonly ICrudRepository<DisbursementMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDisbursementMethodCommandHandler(ICrudRepository<DisbursementMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto?> Handle(UpdateDisbursementMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IsActive = request.Request.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
