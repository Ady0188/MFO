using MediatR;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Queries;

public sealed record GetLoanByIdQuery(Guid Id) : IRequest<LoanDto?>;

public sealed class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto?>
{
    private readonly ILoanRepository _loanRepository;

    public GetLoanByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

        return loan is null ? null : LoanMappings.ToDto(loan);
    }
}
