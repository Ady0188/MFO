namespace MFO.Application.ReferenceData;

public sealed record CurrencyRequest(
    string Code,
    string NumericCode,
    string Name,
    string Symbol,
    bool IsActive);
