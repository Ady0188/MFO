namespace MFO.Application.ReferenceData;

public sealed record CurrencyDto(
    Guid Id,
    string Code,
    string Name,
    string Symbol,
    bool IsActive);
