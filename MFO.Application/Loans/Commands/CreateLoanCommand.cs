using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.Loans.Commands;

public sealed record CreateLoanCommand(CreateLoanRequest Request) : IRequest<CommandResult<LoanDto>>;

public sealed class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, CommandResult<LoanDto>>
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanCommandHandler(
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

    public async Task<CommandResult<LoanDto>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        if (await _loanRepository.LoanNumberExistsAsync(request.Request.LoanNumber, null, cancellationToken))
        {
            return CommandResult<LoanDto>.Conflict();
        }

        var missing = await GetMissingReferencesAsync(request.Request, cancellationToken);
        if (missing.Count > 0)
        {
            return CommandResult<LoanDto>.Missing(missing);
        }

        var loan = Loan.Create(
            request.Request.LoanNumber,
            request.Request.CustomerId,
            request.Request.ProductId,
            request.Request.StatusId,
            request.Request.CurrencyId,
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

        await _loanRepository.AddAsync(loan, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanDto>.Success(LoanMappings.ToDto(loan));
    }

    private async Task<List<string>> GetMissingReferencesAsync(CreateLoanRequest request, CancellationToken cancellationToken)
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
