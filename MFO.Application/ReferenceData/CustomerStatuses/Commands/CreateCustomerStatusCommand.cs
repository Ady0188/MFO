using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerStatuses.Commands;

public sealed record CreateCustomerStatusCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateCustomerStatusCommandHandler : IRequestHandler<CreateCustomerStatusCommand, ReferenceItemDto>
{
    private readonly IAppDbContext _dbContext;

    public CreateCustomerStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
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

        _dbContext.CustomerStatuses.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
