namespace MFO.Application.ReferenceData;

public sealed record ReferenceItemDto(
    Guid Id,
    string Code,
    string Name,
    bool IsActive);
