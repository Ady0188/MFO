using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;

namespace MFO.Application.Loans.Commands;

public sealed record UpdateLoanCommand(Guid Id, UpdateLoanRequest Request) : IRequest<CommandResult<LoanDto>>;

public sealed class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, CommandResult<LoanDto>>
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLoanCommandHandler(
        ILoanRepository loanRepository,
        ICustomerRepository customerRepository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _customerRepository = customerRepository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanDto>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);
        if (loan is null)
        {
            return CommandResult<LoanDto>.NotFound();
        }

        if (!string.Equals(loan.LoanNumber, request.Request.LoanNumber, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _loanRepository.LoanNumberExistsAsync(request.Request.LoanNumber, request.Id, cancellationToken);
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

        loan.Update(
            request.Request.LoanNumber,
            request.Request.CustomerId,
            request.Request.ProductId,
            request.Request.StatusId,
            request.Request.CurrencyId,
            request.Request.BranchId,
            request.Request.CuratorId,
            request.Request.DisbursementMethodId,
            request.Request.RepaymentMethodId,
            request.Request.PurposeId,
            request.Request.PaymentFrequencyId,
            request.Request.PenaltyPolicyId,
            request.Request.PrincipalAmount,
            request.Request.InterestRate,
            request.Request.FeesAmount,
            request.Request.PenaltyRate,
            request.Request.TotalPayable,
            request.Request.OutstandingPrincipal,
            request.Request.OutstandingInterest,
            request.Request.TermMonths,
            request.Request.IssuedOn,
            request.Request.ApprovedOn,
            request.Request.DisbursedOn,
            request.Request.MaturityOn,
            request.Request.ClosedOn);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanDto>.Success(LoanMappings.ToDto(loan));
    }

    private async Task<List<string>> GetMissingReferencesAsync(UpdateLoanRequest request, CancellationToken cancellationToken)
    {
        var missing = new List<string>();

        if (!await _customerRepository.ExistsAsync(request.CustomerId, cancellationToken))
        {
            missing.Add("Customer");
        }

        if (!await _referenceLookup.LoanProductExistsAsync(request.ProductId, cancellationToken))
        {
            missing.Add("LoanProduct");
        }

        if (!await _referenceLookup.LoanStatusExistsAsync(request.StatusId, cancellationToken))
        {
            missing.Add("LoanStatus");
        }

        if (!await _referenceLookup.CurrencyExistsAsync(request.CurrencyId, cancellationToken))
        {
            missing.Add("Currency");
        }

        if (!await _referenceLookup.BranchExistsAsync(request.BranchId, cancellationToken))
        {
            missing.Add("Branch");
        }

        if (!await _referenceLookup.EmployeeExistsAsync(request.CuratorId, cancellationToken))
        {
            missing.Add("Curator");
        }

        if (!await _referenceLookup.DisbursementMethodExistsAsync(request.DisbursementMethodId, cancellationToken))
        {
            missing.Add("DisbursementMethod");
        }

        if (!await _referenceLookup.RepaymentMethodExistsAsync(request.RepaymentMethodId, cancellationToken))
        {
            missing.Add("RepaymentMethod");
        }

        if (!await _referenceLookup.PurposeExistsAsync(request.PurposeId, cancellationToken))
        {
            missing.Add("Purpose");
        }

        if (!await _referenceLookup.PaymentFrequencyExistsAsync(request.PaymentFrequencyId, cancellationToken))
        {
            missing.Add("PaymentFrequency");
        }

        if (!await _referenceLookup.PenaltyPolicyExistsAsync(request.PenaltyPolicyId, cancellationToken))
        {
            missing.Add("PenaltyPolicy");
        }

        return missing;
    }
}
