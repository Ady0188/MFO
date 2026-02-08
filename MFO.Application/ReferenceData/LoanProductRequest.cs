namespace MFO.Application.ReferenceData;

public sealed record LoanProductRequest(
    string Code,
    string Name,
    decimal InterestRate,
    decimal OriginationFee,
    decimal MinAmount,
    decimal MaxAmount,
    int MinTermMonths,
    int MaxTermMonths,
    Guid CurrencyId,
    Guid PaymentFrequencyId,
    Guid PenaltyPolicyId,
    bool IsActive);
