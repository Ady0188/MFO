using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Queries;

public sealed record GetLoanStatusesQuery : IRequest<IReadOnlyList<LoanStatusDto>>;

public sealed class GetLoanStatusesQueryHandler : IRequestHandler<GetLoanStatusesQuery, IReadOnlyList<LoanStatusDto>>
{
    private readonly ICrudRepository<LoanStatus> _repository;

    public GetLoanStatusesQueryHandler(ICrudRepository<LoanStatus> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LoanStatusDto>> Handle(GetLoanStatusesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new LoanStatusDto(x.Id, x.Code, x.Name, x.IsClosed)).ToList();
    }
}
