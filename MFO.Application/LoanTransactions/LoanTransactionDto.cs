namespace MFO.Application.LoanTransactions;

public sealed record LoanTransactionDto(
    Guid Id,
    Guid LoanId,
    Guid LoanAccountId,
    Guid TransactionTypeId,
    decimal Amount,
    DateOnly OccurredOn,
    string Description,
    DateTime CreatedAt);
