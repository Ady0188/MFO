using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerStatuses.Queries;

public sealed record GetCustomerStatusesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetCustomerStatusesQueryHandler : IRequestHandler<GetCustomerStatusesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<CustomerStatus> _repository;

    public GetCustomerStatusesQueryHandler(ICrudRepository<CustomerStatus> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetCustomerStatusesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
