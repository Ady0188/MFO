using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.LoanAccounts.Commands;

public sealed record UpdateLoanAccountCommand(Guid Id, LoanAccountRequest Request) : IRequest<CommandResult<LoanAccountDto>>;

public sealed class UpdateLoanAccountCommandHandler : IRequestHandler<UpdateLoanAccountCommand, CommandResult<LoanAccountDto>>
{
    private readonly ILoanAccountRepository _accountRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLoanAccountCommandHandler(
        ILoanAccountRepository accountRepository,
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanAccountDto>> Handle(UpdateLoanAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return CommandResult<LoanAccountDto>.NotFound();
        }

        var loan = await _loanRepository.GetByIdAsNoTrackingAsync(request.Request.LoanId, cancellationToken);
        if (loan is null)
        {
            return CommandResult<LoanAccountDto>.Missing(new[] { "Loan" });
        }

        if (await _accountRepository.AccountNumberExistsAsync(request.Request.AccountNumber, request.Id, cancellationToken))
        {
            return CommandResult<LoanAccountDto>.Conflict();
        }

        entity.LoanId = request.Request.LoanId;
        entity.AccountNumber = request.Request.AccountNumber;
        entity.Balance = request.Request.Balance;
        entity.OpenedOn = request.Request.OpenedOn;
        entity.ClosedOn = request.Request.ClosedOn;
        entity.IsActive = request.Request.IsActive;

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
