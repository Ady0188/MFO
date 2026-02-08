using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.LoanAccounts.Commands;

public sealed record DeleteLoanAccountCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanAccountCommandHandler : IRequestHandler<DeleteLoanAccountCommand, bool>
{
    private readonly ILoanAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanAccountCommandHandler(ILoanAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await _accountRepository.RemoveAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
