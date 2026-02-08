using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Queries;

public sealed record GetDisbursementMethodByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetDisbursementMethodByIdQueryHandler : IRequestHandler<GetDisbursementMethodByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<DisbursementMethod> _repository;

    public GetDisbursementMethodByIdQueryHandler(ICrudRepository<DisbursementMethod> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetDisbursementMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
