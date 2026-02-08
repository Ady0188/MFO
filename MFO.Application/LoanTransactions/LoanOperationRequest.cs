namespace MFO.Application.LoanTransactions;

public sealed record LoanOperationRequest(
    Guid LoanId,
    Guid LoanAccountId,
    decimal Amount,
    DateOnly OccurredOn,
    string Description);
