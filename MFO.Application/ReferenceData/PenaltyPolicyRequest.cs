namespace MFO.Application.ReferenceData;

public sealed record PenaltyPolicyRequest(
    string Code,
    string Name,
    decimal PenaltyRate,
    decimal FixedFee,
    bool IsActive);
