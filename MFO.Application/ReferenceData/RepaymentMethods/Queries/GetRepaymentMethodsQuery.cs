using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Queries;

public sealed record GetRepaymentMethodsQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetRepaymentMethodsQueryHandler : IRequestHandler<GetRepaymentMethodsQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<RepaymentMethod> _repository;

    public GetRepaymentMethodsQueryHandler(ICrudRepository<RepaymentMethod> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetRepaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
