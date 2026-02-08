namespace MFO.Application.ReferenceData;

public sealed record PenaltyPolicyDto(
    Guid Id,
    string Code,
    string Name,
    decimal PenaltyRate,
    decimal FixedFee,
    bool IsActive);
