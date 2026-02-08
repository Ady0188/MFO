using MediatR;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Commands;

public sealed record DeleteLoanCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, bool>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanCommandHandler(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);
        if (loan is null)
        {
            return false;
        }

        await _loanRepository.RemoveAsync(loan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
