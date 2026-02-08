namespace MFO.Application.ReferenceData;

public sealed record CurrencyDto(
    Guid Id,
    string Code,
    string NumericCode,
    string Name,
    string Symbol,
    bool IsActive);
