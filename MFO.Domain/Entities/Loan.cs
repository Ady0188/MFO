using MFO.Domain.Common;
using System;

namespace MFO.Domain.Entities;

public sealed class Loan : IAggregateRoot
{
    private Loan()
    {
    }

    public Guid Id { get; private set; }
    public string LoanNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public Guid ProductId { get; private set; }
    public LoanProduct Product { get; private set; } = null!;
    public Guid StatusId { get; private set; }
    public LoanStatus Status { get; private set; } = null!;
    public Guid CurrencyId { get; private set; }
    public Currency Currency { get; private set; } = null!;
    public Guid BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;
    public Guid CuratorId { get; private set; }
    public Employee Curator { get; private set; } = null!;
    public Guid DisbursementMethodId { get; private set; }
    public DisbursementMethod DisbursementMethod { get; private set; } = null!;
    public Guid RepaymentMethodId { get; private set; }
    public RepaymentMethod RepaymentMethod { get; private set; } = null!;
    public Guid PurposeId { get; private set; }
    public Purpose Purpose { get; private set; } = null!;
    public Guid PaymentFrequencyId { get; private set; }
    public PaymentFrequency PaymentFrequency { get; private set; } = null!;
    public Guid PenaltyPolicyId { get; private set; }
    public PenaltyPolicy PenaltyPolicy { get; private set; } = null!;
    public decimal PrincipalAmount { get; private set; }
    public decimal InterestRate { get; private set; }
    public decimal FeesAmount { get; private set; }
    public decimal PenaltyRate { get; private set; }
    public decimal TotalPayable { get; private set; }
    public decimal OutstandingPrincipal { get; private set; }
    public decimal OutstandingInterest { get; private set; }
    public int TermMonths { get; private set; }
    public DateOnly IssuedOn { get; private set; }
    public DateOnly? ApprovedOn { get; private set; }
    public DateOnly? DisbursedOn { get; private set; }
    public DateOnly? MaturityOn { get; private set; }
    public DateOnly? ClosedOn { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static Loan Create(
        string loanNumber,
        Guid customerId,
        Guid productId,
        Guid statusId,
        Guid currencyId,
        Guid branchId,
        Guid curatorId,
        Guid disbursementMethodId,
        Guid repaymentMethodId,
        Guid purposeId,
        Guid paymentFrequencyId,
        Guid penaltyPolicyId,
        decimal principalAmount,
        decimal interestRate,
        decimal feesAmount,
        decimal penaltyRate,
        decimal totalPayable,
        decimal outstandingPrincipal,
        decimal outstandingInterest,
        int termMonths,
        DateOnly issuedOn,
        DateOnly? approvedOn,
        DateOnly? disbursedOn,
        DateOnly? maturityOn,
        DateOnly? closedOn)
    {
        return new Loan
        {
            Id = Guid.NewGuid(),
            LoanNumber = loanNumber,
            CustomerId = customerId,
            ProductId = productId,
            StatusId = statusId,
            CurrencyId = currencyId,
            BranchId = branchId,
            CuratorId = curatorId,
            DisbursementMethodId = disbursementMethodId,
            RepaymentMethodId = repaymentMethodId,
            PurposeId = purposeId,
            PaymentFrequencyId = paymentFrequencyId,
            PenaltyPolicyId = penaltyPolicyId,
            PrincipalAmount = principalAmount,
            InterestRate = interestRate,
            FeesAmount = feesAmount,
            PenaltyRate = penaltyRate,
            TotalPayable = totalPayable,
            OutstandingPrincipal = outstandingPrincipal,
            OutstandingInterest = outstandingInterest,
            TermMonths = termMonths,
            IssuedOn = issuedOn,
            ApprovedOn = approvedOn,
            DisbursedOn = disbursedOn,
            MaturityOn = maturityOn,
            ClosedOn = closedOn,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string loanNumber,
        Guid customerId,
        Guid productId,
        Guid statusId,
        Guid currencyId,
        Guid branchId,
        Guid curatorId,
        Guid disbursementMethodId,
        Guid repaymentMethodId,
        Guid purposeId,
        Guid paymentFrequencyId,
        Guid penaltyPolicyId,
        decimal principalAmount,
        decimal interestRate,
        decimal feesAmount,
        decimal penaltyRate,
        decimal totalPayable,
        decimal outstandingPrincipal,
        decimal outstandingInterest,
        int termMonths,
        DateOnly issuedOn,
        DateOnly? approvedOn,
        DateOnly? disbursedOn,
        DateOnly? maturityOn,
        DateOnly? closedOn)
    {
        LoanNumber = loanNumber;
        CustomerId = customerId;
        ProductId = productId;
        StatusId = statusId;
        CurrencyId = currencyId;
        BranchId = branchId;
        CuratorId = curatorId;
        DisbursementMethodId = disbursementMethodId;
        RepaymentMethodId = repaymentMethodId;
        PurposeId = purposeId;
        PaymentFrequencyId = paymentFrequencyId;
        PenaltyPolicyId = penaltyPolicyId;
        PrincipalAmount = principalAmount;
        InterestRate = interestRate;
        FeesAmount = feesAmount;
        PenaltyRate = penaltyRate;
        TotalPayable = totalPayable;
        OutstandingPrincipal = outstandingPrincipal;
        OutstandingInterest = outstandingInterest;
        TermMonths = termMonths;
        IssuedOn = issuedOn;
        ApprovedOn = approvedOn;
        DisbursedOn = disbursedOn;
        MaturityOn = maturityOn;
        ClosedOn = closedOn;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkDisbursed(Guid statusId, DateOnly disbursedOn)
    {
        StatusId = statusId;
        DisbursedOn = disbursedOn;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ApplyRepayment(decimal amount, Guid? closedStatusId, DateOnly occurredOn)
    {
        if (amount <= 0)
        {
            return;
        }

        OutstandingPrincipal = Math.Max(0m, OutstandingPrincipal - amount);

        if (OutstandingPrincipal == 0m && closedStatusId.HasValue)
        {
            StatusId = closedStatusId.Value;
            ClosedOn = occurredOn;
        }

        UpdatedAt = DateTime.UtcNow;
    }
}
