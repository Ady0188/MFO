using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record CreateDisbursementMethodCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateDisbursementMethodCommandHandler : IRequestHandler<CreateDisbursementMethodCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<DisbursementMethod> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDisbursementMethodCommandHandler(ICrudRepository<DisbursementMethod> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateDisbursementMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = new DisbursementMethod
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
