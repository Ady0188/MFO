using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Queries;

public sealed record GetRepaymentMethodByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetRepaymentMethodByIdQueryHandler : IRequestHandler<GetRepaymentMethodByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<RepaymentMethod> _repository;

    public GetRepaymentMethodByIdQueryHandler(ICrudRepository<RepaymentMethod> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetRepaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
