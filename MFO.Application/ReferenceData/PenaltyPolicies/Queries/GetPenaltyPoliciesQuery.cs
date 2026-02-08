using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.PenaltyPolicies.Queries;

public sealed record GetPenaltyPoliciesQuery : IRequest<IReadOnlyList<PenaltyPolicyDto>>;

public sealed class GetPenaltyPoliciesQueryHandler : IRequestHandler<GetPenaltyPoliciesQuery, IReadOnlyList<PenaltyPolicyDto>>
{
    private readonly ICrudRepository<PenaltyPolicy> _repository;

    public GetPenaltyPoliciesQueryHandler(ICrudRepository<PenaltyPolicy> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PenaltyPolicyDto>> Handle(GetPenaltyPoliciesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new PenaltyPolicyDto(x.Id, x.Code, x.Name, x.PenaltyRate, x.FixedFee, x.IsActive)).ToList();
    }
}
