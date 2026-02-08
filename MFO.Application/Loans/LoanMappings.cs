using MFO.Domain.Entities;

namespace MFO.Application.Loans;

public static class LoanMappings
{
    public static LoanDto ToDto(Loan loan)
    {
        return new LoanDto(
            loan.Id,
            loan.LoanNumber,
            loan.CustomerId,
            loan.ProductId,
            loan.StatusId,
            loan.CurrencyId,
            loan.BranchId,
            loan.CuratorId,
            loan.DisbursementMethodId,
            loan.RepaymentMethodId,
            loan.PurposeId,
            loan.PaymentFrequencyId,
            loan.PenaltyPolicyId,
            loan.PrincipalAmount,
            loan.InterestRate,
            loan.FeesAmount,
            loan.PenaltyRate,
            loan.TotalPayable,
            loan.OutstandingPrincipal,
            loan.OutstandingInterest,
            loan.TermMonths,
            loan.IssuedOn,
            loan.ApprovedOn,
            loan.DisbursedOn,
            loan.MaturityOn,
            loan.ClosedOn,
            loan.CreatedAt,
            loan.UpdatedAt);
    }
}
