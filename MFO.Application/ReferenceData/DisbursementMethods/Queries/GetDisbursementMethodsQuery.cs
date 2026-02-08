using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Queries;

public sealed record GetDisbursementMethodsQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetDisbursementMethodsQueryHandler : IRequestHandler<GetDisbursementMethodsQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<DisbursementMethod> _repository;

    public GetDisbursementMethodsQueryHandler(ICrudRepository<DisbursementMethod> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetDisbursementMethodsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
