using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Purposes.Queries;

public sealed record GetPurposesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetPurposesQueryHandler : IRequestHandler<GetPurposesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<Purpose> _repository;

    public GetPurposesQueryHandler(ICrudRepository<Purpose> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetPurposesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
