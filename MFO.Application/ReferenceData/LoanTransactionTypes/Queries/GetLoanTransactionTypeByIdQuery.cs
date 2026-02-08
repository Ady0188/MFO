using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanTransactionTypes.Queries;

public sealed record GetLoanTransactionTypeByIdQuery(Guid Id) : IRequest<ReferenceItemDto?>;

public sealed class GetLoanTransactionTypeByIdQueryHandler : IRequestHandler<GetLoanTransactionTypeByIdQuery, ReferenceItemDto?>
{
    private readonly ICrudRepository<LoanTransactionType> _repository;

    public GetLoanTransactionTypeByIdQueryHandler(ICrudRepository<LoanTransactionType> repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceItemDto?> Handle(GetLoanTransactionTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null ? null : new ReferenceItemDto(item.Id, item.Code, item.Name, item.IsActive);
    }
}
