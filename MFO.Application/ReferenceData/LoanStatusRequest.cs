namespace MFO.Application.ReferenceData;

public sealed record LoanStatusRequest(
    string Code,
    string Name,
    bool IsClosed);
