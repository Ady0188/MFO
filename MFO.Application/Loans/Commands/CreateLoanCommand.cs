using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.Loans.Commands;

public sealed record CreateLoanCommand(CreateLoanRequest Request) : IRequest<CommandResult<LoanDto>>;

public sealed class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, CommandResult<LoanDto>>
{
    private readonly IAppDbContext _dbContext;

    public CreateLoanCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CommandResult<LoanDto>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Loans.AnyAsync(x => x.LoanNumber == request.Request.LoanNumber, cancellationToken))
        {
            return CommandResult<LoanDto>.Conflict();
        }

        var missing = await GetMissingReferencesAsync(request.Request, cancellationToken);
        if (missing.Count > 0)
        {
            return CommandResult<LoanDto>.Missing(missing);
        }

        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanNumber = request.Request.LoanNumber,
            CustomerId = request.Request.CustomerId,
            ProductId = request.Request.ProductId,
            StatusId = request.Request.StatusId,
            CurrencyId = request.Request.CurrencyId,
            DisbursementMethodId = request.Request.DisbursementMethodId,
            RepaymentMethodId = request.Request.RepaymentMethodId,
            PurposeId = request.Request.PurposeId,
            PaymentFrequencyId = request.Request.PaymentFrequencyId,
            PenaltyPolicyId = request.Request.PenaltyPolicyId,
            PrincipalAmount = request.Request.PrincipalAmount,
            InterestRate = request.Request.InterestRate,
            FeesAmount = request.Request.FeesAmount,
            PenaltyRate = request.Request.PenaltyRate,
            TotalPayable = request.Request.TotalPayable,
            OutstandingPrincipal = request.Request.OutstandingPrincipal,
            OutstandingInterest = request.Request.OutstandingInterest,
            TermMonths = request.Request.TermMonths,
            IssuedOn = request.Request.IssuedOn,
            ApprovedOn = request.Request.ApprovedOn,
            DisbursedOn = request.Request.DisbursedOn,
            MaturityOn = request.Request.MaturityOn,
            ClosedOn = request.Request.ClosedOn,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Loans.Add(loan);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanDto>.Success(LoanMappings.ToDto(loan));
    }

    private async Task<List<string>> GetMissingReferencesAsync(CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var missing = new List<string>();

        if (!await _dbContext.Customers.AnyAsync(x => x.Id == request.CustomerId, cancellationToken))
        {
            missing.Add("Customer");
        }

        if (!await _dbContext.LoanProducts.AnyAsync(x => x.Id == request.ProductId, cancellationToken))
        {
            missing.Add("LoanProduct");
        }

        if (!await _dbContext.LoanStatuses.AnyAsync(x => x.Id == request.StatusId, cancellationToken))
        {
            missing.Add("LoanStatus");
        }

        if (!await _dbContext.Currencies.AnyAsync(x => x.Id == request.CurrencyId, cancellationToken))
        {
            missing.Add("Currency");
        }

        if (!await _dbContext.DisbursementMethods.AnyAsync(x => x.Id == request.DisbursementMethodId, cancellationToken))
        {
            missing.Add("DisbursementMethod");
        }

        if (!await _dbContext.RepaymentMethods.AnyAsync(x => x.Id == request.RepaymentMethodId, cancellationToken))
        {
            missing.Add("RepaymentMethod");
        }

        if (!await _dbContext.Purposes.AnyAsync(x => x.Id == request.PurposeId, cancellationToken))
        {
            missing.Add("Purpose");
        }

        if (!await _dbContext.PaymentFrequencies.AnyAsync(x => x.Id == request.PaymentFrequencyId, cancellationToken))
        {
            missing.Add("PaymentFrequency");
        }

        if (!await _dbContext.PenaltyPolicies.AnyAsync(x => x.Id == request.PenaltyPolicyId, cancellationToken))
        {
            missing.Add("PenaltyPolicy");
        }

        return missing;
    }
}
