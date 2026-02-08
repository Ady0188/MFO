using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Branches.Commands;

public sealed record CreateBranchCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<Branch> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranchCommandHandler(ICrudRepository<Branch> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new Branch
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
