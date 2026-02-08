using MediatR;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.Loans.Queries;

public sealed record GetLoansQuery : IRequest<IReadOnlyList<LoanDto>>;

public sealed class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, IReadOnlyList<LoanDto>>
{
    private readonly ILoanRepository _loanRepository;

    public GetLoansQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IReadOnlyList<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await _loanRepository.GetAllAsync(cancellationToken);
        return loans.Select(LoanMappings.ToDto).ToList();
    }
}
