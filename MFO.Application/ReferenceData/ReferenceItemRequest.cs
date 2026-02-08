namespace MFO.Application.ReferenceData;

public sealed record ReferenceItemRequest(
    string Code,
    string Name,
    bool IsActive);
