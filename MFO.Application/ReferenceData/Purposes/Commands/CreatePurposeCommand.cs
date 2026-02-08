using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Purposes.Commands;

public sealed record CreatePurposeCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreatePurposeCommandHandler : IRequestHandler<CreatePurposeCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<Purpose> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurposeCommandHandler(ICrudRepository<Purpose> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreatePurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Purpose
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
