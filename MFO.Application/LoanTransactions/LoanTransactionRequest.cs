namespace MFO.Application.LoanTransactions;

public sealed record LoanTransactionRequest(
    Guid LoanId,
    Guid LoanAccountId,
    Guid TransactionTypeId,
    decimal Amount,
    DateOnly OccurredOn,
    string Description);
