using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Queries;

public sealed record GetLoanTransactionsQuery : IRequest<IReadOnlyList<LoanTransactionDto>>;

public sealed class GetLoanTransactionsQueryHandler : IRequestHandler<GetLoanTransactionsQuery, IReadOnlyList<LoanTransactionDto>>
{
    private readonly ICrudRepository<LoanTransaction> _repository;

    public GetLoanTransactionsQueryHandler(ICrudRepository<LoanTransaction> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LoanTransactionDto>> Handle(GetLoanTransactionsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new LoanTransactionDto(
            x.Id,
            x.LoanId,
            x.LoanAccountId,
            x.TransactionTypeId,
            x.Amount,
            x.OccurredOn,
            x.Description,
            x.CreatedAt)).ToList();
    }
}
