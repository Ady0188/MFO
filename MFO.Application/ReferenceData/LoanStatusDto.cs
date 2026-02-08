namespace MFO.Application.ReferenceData;

public sealed record LoanStatusDto(
    Guid Id,
    string Code,
    string Name,
    bool IsClosed);
