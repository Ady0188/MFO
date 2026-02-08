using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Queries;

public sealed record GetLoanTransactionByIdQuery(Guid Id) : IRequest<LoanTransactionDto?>;

public sealed class GetLoanTransactionByIdQueryHandler : IRequestHandler<GetLoanTransactionByIdQuery, LoanTransactionDto?>
{
    private readonly ICrudRepository<LoanTransaction> _repository;

    public GetLoanTransactionByIdQueryHandler(ICrudRepository<LoanTransaction> repository)
    {
        _repository = repository;
    }

    public async Task<LoanTransactionDto?> Handle(GetLoanTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null
            ? null
            : new LoanTransactionDto(
                item.Id,
                item.LoanId,
                item.LoanAccountId,
                item.TransactionTypeId,
                item.Amount,
                item.OccurredOn,
                item.Description,
                item.CreatedAt);
    }
}
