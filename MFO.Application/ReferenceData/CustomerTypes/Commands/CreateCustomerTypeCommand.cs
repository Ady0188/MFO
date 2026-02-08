using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerTypes.Commands;

public sealed record CreateCustomerTypeCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateCustomerTypeCommandHandler : IRequestHandler<CreateCustomerTypeCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<CustomerType> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerTypeCommandHandler(ICrudRepository<CustomerType> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new CustomerType
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
