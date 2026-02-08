using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.CustomerTypes.Queries;

public sealed record GetCustomerTypesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetCustomerTypesQueryHandler : IRequestHandler<GetCustomerTypesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<CustomerType> _repository;

    public GetCustomerTypesQueryHandler(ICrudRepository<CustomerType> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetCustomerTypesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
