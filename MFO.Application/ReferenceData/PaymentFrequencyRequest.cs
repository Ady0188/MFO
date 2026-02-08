namespace MFO.Application.ReferenceData;

public sealed record PaymentFrequencyRequest(
    string Code,
    string Name,
    int IntervalDays,
    bool IsActive);
