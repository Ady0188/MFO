using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;

namespace MFO.Application.Loans.Commands;

public sealed record UpdateLoanCommand(Guid Id, UpdateLoanRequest Request) : IRequest<CommandResult<LoanDto>>;

public sealed class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, CommandResult<LoanDto>>
{
    private readonly IAppDbContext _dbContext;

    public UpdateLoanCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CommandResult<LoanDto>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (loan is null)
        {
            return CommandResult<LoanDto>.NotFound();
        }

        if (!string.Equals(loan.LoanNumber, request.Request.LoanNumber, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _dbContext.Loans.AnyAsync(x => x.LoanNumber == request.Request.LoanNumber && x.Id != request.Id, cancellationToken);
            if (exists)
            {
                return CommandResult<LoanDto>.Conflict();
            }
        }

        var missing = await GetMissingReferencesAsync(request.Request, cancellationToken);
        if (missing.Count > 0)
        {
            return CommandResult<LoanDto>.Missing(missing);
        }

        loan.LoanNumber = request.Request.LoanNumber;
        loan.CustomerId = request.Request.CustomerId;
        loan.ProductId = request.Request.ProductId;
        loan.StatusId = request.Request.StatusId;
        loan.CurrencyId = request.Request.CurrencyId;
        loan.DisbursementMethodId = request.Request.DisbursementMethodId;
        loan.RepaymentMethodId = request.Request.RepaymentMethodId;
        loan.PurposeId = request.Request.PurposeId;
        loan.PaymentFrequencyId = request.Request.PaymentFrequencyId;
        loan.PenaltyPolicyId = request.Request.PenaltyPolicyId;
        loan.PrincipalAmount = request.Request.PrincipalAmount;
        loan.InterestRate = request.Request.InterestRate;
        loan.FeesAmount = request.Request.FeesAmount;
        loan.PenaltyRate = request.Request.PenaltyRate;
        loan.TotalPayable = request.Request.TotalPayable;
        loan.OutstandingPrincipal = request.Request.OutstandingPrincipal;
        loan.OutstandingInterest = request.Request.OutstandingInterest;
        loan.TermMonths = request.Request.TermMonths;
        loan.IssuedOn = request.Request.IssuedOn;
        loan.ApprovedOn = request.Request.ApprovedOn;
        loan.DisbursedOn = request.Request.DisbursedOn;
        loan.MaturityOn = request.Request.MaturityOn;
        loan.ClosedOn = request.Request.ClosedOn;
        loan.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanDto>.Success(LoanMappings.ToDto(loan));
    }

    private async Task<List<string>> GetMissingReferencesAsync(UpdateLoanRequest request, CancellationToken cancellationToken)
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
