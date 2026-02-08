using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Purposes.Queries;

public sealed record GetPurposeByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetPurposeByIdQueryHandler : IRequestHandler<GetPurposeByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<Purpose> _repository;

    public GetPurposeByIdQueryHandler(ICrudRepository<Purpose> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetPurposeByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
