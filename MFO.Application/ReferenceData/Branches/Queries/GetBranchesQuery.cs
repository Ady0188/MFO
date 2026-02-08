using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Branches.Queries;

public sealed record GetBranchesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<Branch> _repository;

    public GetBranchesQueryHandler(ICrudRepository<Branch> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
