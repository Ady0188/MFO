using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Commands;

public sealed record CreateRepaymentCommand(LoanOperationRequest Request) : IRequest<CommandResult<LoanTransactionDto>>;

public sealed class CreateRepaymentCommandHandler : IRequestHandler<CreateRepaymentCommand, CommandResult<LoanTransactionDto>>
{
    private const string TransactionTypeCode = "REPAYMENT";
    private const string ClosedStatusCode = "CLOSED";

    private readonly ICrudRepository<LoanTransaction> _transactionRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly ILoanAccountRepository _accountRepository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRepaymentCommandHandler(
        ICrudRepository<LoanTransaction> transactionRepository,
        ILoanRepository loanRepository,
        ILoanAccountRepository accountRepository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _loanRepository = loanRepository;
        _accountRepository = accountRepository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanTransactionDto>> Handle(CreateRepaymentCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Request.LoanId, cancellationToken);
        if (loan is null)
        {
            return CommandResult<LoanTransactionDto>.NotFound();
        }

        var account = await _accountRepository.GetByIdAsync(request.Request.LoanAccountId, cancellationToken);
        if (account is null)
        {
            return CommandResult<LoanTransactionDto>.Missing(new[] { "LoanAccount" });
        }

        if (account.LoanId != loan.Id)
        {
            return CommandResult<LoanTransactionDto>.Conflict();
        }

        var transactionTypeId = await _referenceLookup.GetLoanTransactionTypeIdByCodeAsync(TransactionTypeCode, cancellationToken);
        if (!transactionTypeId.HasValue)
        {
            return CommandResult<LoanTransactionDto>.Missing(new[] { "LoanTransactionType" });
        }

        var closedStatusId = await _referenceLookup.GetLoanStatusIdByCodeAsync(ClosedStatusCode, cancellationToken);

        account.Balance -= request.Request.Amount;
        loan.ApplyRepayment(request.Request.Amount, closedStatusId, request.Request.OccurredOn);

        var transaction = new LoanTransaction
        {
            Id = Guid.NewGuid(),
            LoanId = loan.Id,
            LoanAccountId = account.Id,
            TransactionTypeId = transactionTypeId.Value,
            Amount = request.Request.Amount,
            OccurredOn = request.Request.OccurredOn,
            Description = request.Request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanTransactionDto>.Success(new LoanTransactionDto(
            transaction.Id,
            transaction.LoanId,
            transaction.LoanAccountId,
            transaction.TransactionTypeId,
            transaction.Amount,
            transaction.OccurredOn,
            transaction.Description,
            transaction.CreatedAt));
    }
}
