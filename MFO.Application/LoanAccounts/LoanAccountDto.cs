namespace MFO.Application.LoanAccounts;

public sealed record LoanAccountDto(
    Guid Id,
    Guid LoanId,
    string AccountNumber,
    decimal Balance,
    DateOnly OpenedOn,
    DateOnly? ClosedOn,
    bool IsActive);
