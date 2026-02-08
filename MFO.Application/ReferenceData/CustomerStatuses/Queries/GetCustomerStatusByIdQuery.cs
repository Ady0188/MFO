using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerStatuses.Queries;

public sealed record GetCustomerStatusByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetCustomerStatusByIdQueryHandler : IRequestHandler<GetCustomerStatusByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<CustomerStatus> _repository;

    public GetCustomerStatusByIdQueryHandler(ICrudRepository<CustomerStatus> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetCustomerStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
