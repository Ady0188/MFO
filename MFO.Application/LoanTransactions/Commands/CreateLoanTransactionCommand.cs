using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.LoanTransactions.Commands;

public sealed record CreateLoanTransactionCommand(LoanTransactionRequest Request) : IRequest<CommandResult<LoanTransactionDto>>;

public sealed class CreateLoanTransactionCommandHandler : IRequestHandler<CreateLoanTransactionCommand, CommandResult<LoanTransactionDto>>
{
    private readonly ICrudRepository<LoanTransaction> _repository;
    private readonly ILoanRepository _loanRepository;
    private readonly ILoanAccountRepository _accountRepository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanTransactionCommandHandler(
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

    public async Task<CommandResult<LoanTransactionDto>> Handle(CreateLoanTransactionCommand request, CancellationToken cancellationToken)
    {
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

        var entity = new LoanTransaction
        {
            Id = Guid.NewGuid(),
            LoanId = request.Request.LoanId,
            LoanAccountId = request.Request.LoanAccountId,
            TransactionTypeId = request.Request.TransactionTypeId,
            Amount = request.Request.Amount,
            OccurredOn = request.Request.OccurredOn,
            Description = request.Request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity, cancellationToken);
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
