using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerStatuses.Commands;

public sealed record CreateCustomerStatusCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateCustomerStatusCommandHandler : IRequestHandler<CreateCustomerStatusCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<CustomerStatus> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerStatusCommandHandler(ICrudRepository<CustomerStatus> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new CustomerStatus
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
