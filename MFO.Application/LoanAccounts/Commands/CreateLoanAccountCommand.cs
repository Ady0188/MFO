using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.LoanAccounts.Commands;

public sealed record CreateLoanAccountCommand(LoanAccountRequest Request) : IRequest<CommandResult<LoanAccountDto>>;

public sealed class CreateLoanAccountCommandHandler : IRequestHandler<CreateLoanAccountCommand, CommandResult<LoanAccountDto>>
{
    private readonly ILoanAccountRepository _accountRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanAccountCommandHandler(
        ILoanAccountRepository accountRepository,
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanAccountDto>> Handle(CreateLoanAccountCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsNoTrackingAsync(request.Request.LoanId, cancellationToken);
        if (loan is null)
        {
            return CommandResult<LoanAccountDto>.Missing(new[] { "Loan" });
        }

        if (await _accountRepository.AccountNumberExistsAsync(request.Request.AccountNumber, null, cancellationToken))
        {
            return CommandResult<LoanAccountDto>.Conflict();
        }

        var entity = new LoanAccount
        {
            Id = Guid.NewGuid(),
            LoanId = request.Request.LoanId,
            AccountNumber = request.Request.AccountNumber,
            Balance = request.Request.Balance,
            OpenedOn = request.Request.OpenedOn,
            ClosedOn = request.Request.ClosedOn,
            IsActive = request.Request.IsActive
        };

        await _accountRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanAccountDto>.Success(new LoanAccountDto(
            entity.Id,
            entity.LoanId,
            entity.AccountNumber,
            entity.Balance,
            entity.OpenedOn,
            entity.ClosedOn,
            entity.IsActive));
    }
}
