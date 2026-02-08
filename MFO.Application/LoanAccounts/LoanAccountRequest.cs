namespace MFO.Application.LoanAccounts;

public sealed record LoanAccountRequest(
    Guid LoanId,
    string AccountNumber,
    decimal Balance,
    DateOnly OpenedOn,
    DateOnly? ClosedOn,
    bool IsActive);
