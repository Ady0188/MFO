using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record UpdateRepaymentMethodCommand(Guid Id, ReferenceItemRequest Request) : IRequest<ReferenceItemDto?>;

public sealed class UpdateRepaymentMethodCommandHandler : IRequestHandler<UpdateRepaymentMethodCommand, ReferenceItemDto?>
{
    private readonly ICrudRepository<RepaymentMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRepaymentMethodCommandHandler(ICrudRepository<RepaymentMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto?> Handle(UpdateRepaymentMethodCommand request, CancellationToken cancellationToken)
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
