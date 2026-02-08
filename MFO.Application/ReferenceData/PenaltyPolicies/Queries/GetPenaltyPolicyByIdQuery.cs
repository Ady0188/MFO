using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Queries;

public sealed record GetPenaltyPolicyByIdQuery(Guid Id) : IRequest<PenaltyPolicyDto?>;

public sealed class GetPenaltyPolicyByIdQueryHandler : IRequestHandler<GetPenaltyPolicyByIdQuery, PenaltyPolicyDto?>
{
    private readonly ICrudRepository<PenaltyPolicy> _repository;

    public GetPenaltyPolicyByIdQueryHandler(ICrudRepository<PenaltyPolicy> repository)
    {
        _repository = repository;
    }

    public async Task<PenaltyPolicyDto?> Handle(GetPenaltyPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new PenaltyPolicyDto(item.Id, item.Code, item.Name, item.PenaltyRate, item.FixedFee, item.IsActive);
    }
}
