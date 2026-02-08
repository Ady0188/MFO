using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Queries;

public sealed record GetLoanStatusByIdQuery(Guid Id) : IRequest<LoanStatusDto?>;

public sealed class GetLoanStatusByIdQueryHandler : IRequestHandler<GetLoanStatusByIdQuery, LoanStatusDto?>
{
    private readonly ICrudRepository<LoanStatus> _repository;

    public GetLoanStatusByIdQueryHandler(ICrudRepository<LoanStatus> repository)
    {
        _repository = repository;
    }

    public async Task<LoanStatusDto?> Handle(GetLoanStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return item is null ? null : new LoanStatusDto(item.Id, item.Code, item.Name, item.IsClosed);
    }
}
