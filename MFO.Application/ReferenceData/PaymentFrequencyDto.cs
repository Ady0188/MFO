namespace MFO.Application.ReferenceData;

public sealed record PaymentFrequencyDto(
    Guid Id,
    string Code,
    string Name,
    int IntervalDays,
    bool IsActive);
