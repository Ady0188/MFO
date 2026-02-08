using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Branches.Queries;

public sealed record GetBranchByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<Branch> _repository;

    public GetBranchByIdQueryHandler(ICrudRepository<Branch> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
