using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Commands;

public sealed record UpdateLoanTransactionCommand(Guid Id, LoanTransactionRequest Request) : IRequest<CommandResult<LoanTransactionDto>>;

public sealed class UpdateLoanTransactionCommandHandler : IRequestHandler<UpdateLoanTransactionCommand, CommandResult<LoanTransactionDto>>
{
    private readonly ICrudRepository<LoanTransaction> _repository;
    private readonly ILoanRepository _loanRepository;
    private readonly ILoanAccountRepository _accountRepository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLoanTransactionCommandHandler(
        ICrudRepository<LoanTransaction> repository,
        ILoanRepository loanRepository,
        ILoanAccountRepository accountRepository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _loanRepository = loanRepository;
        _accountRepository = accountRepository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanTransactionDto>> Handle(UpdateLoanTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return CommandResult<LoanTransactionDto>.NotFound();
        }

        var missing = new List<string>();

        if (await _loanRepository.GetByIdAsNoTrackingAsync(request.Request.LoanId, cancellationToken) is null)
        {
            missing.Add("Loan");
        }

        if (await _accountRepository.GetByIdAsNoTrackingAsync(request.Request.LoanAccountId, cancellationToken) is null)
        {
            missing.Add("LoanAccount");
        }

        if (!await _referenceLookup.LoanTransactionTypeExistsAsync(request.Request.TransactionTypeId, cancellationToken))
        {
            missing.Add("TransactionType");
        }

        if (missing.Count > 0)
        {
            return CommandResult<LoanTransactionDto>.Missing(missing);
        }

        entity.LoanId = request.Request.LoanId;
        entity.LoanAccountId = request.Request.LoanAccountId;
        entity.TransactionTypeId = request.Request.TransactionTypeId;
        entity.Amount = request.Request.Amount;
        entity.OccurredOn = request.Request.OccurredOn;
        entity.Description = request.Request.Description;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanTransactionDto>.Success(new LoanTransactionDto(
            entity.Id,
            entity.LoanId,
            entity.LoanAccountId,
            entity.TransactionTypeId,
            entity.Amount,
            entity.OccurredOn,
            entity.Description,
            entity.CreatedAt));
    }
}
