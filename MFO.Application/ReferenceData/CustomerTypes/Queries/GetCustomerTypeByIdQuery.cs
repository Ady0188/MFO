using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerTypes.Queries;

public sealed record GetCustomerTypeByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetCustomerTypeByIdQueryHandler : IRequestHandler<GetCustomerTypeByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<CustomerType> _repository;

    public GetCustomerTypeByIdQueryHandler(ICrudRepository<CustomerType> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetCustomerTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
