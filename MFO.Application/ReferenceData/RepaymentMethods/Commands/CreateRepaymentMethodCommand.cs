using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record CreateRepaymentMethodCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateRepaymentMethodCommandHandler : IRequestHandler<CreateRepaymentMethodCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<RepaymentMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRepaymentMethodCommandHandler(ICrudRepository<RepaymentMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateRepaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = new RepaymentMethod
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsActive = request.Request.IsActive
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
