using MediatR;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.LoanAccounts.Queries;

public sealed record GetLoanAccountsQuery : IRequest<IReadOnlyList<LoanAccountDto>>;

public sealed class GetLoanAccountsQueryHandler : IRequestHandler<GetLoanAccountsQuery, IReadOnlyList<LoanAccountDto>>
{
    private readonly ILoanAccountRepository _repository;

    public GetLoanAccountsQueryHandler(ILoanAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LoanAccountDto>> Handle(GetLoanAccountsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new LoanAccountDto(x.Id, x.LoanId, x.AccountNumber, x.Balance, x.OpenedOn, x.ClosedOn, x.IsActive)).ToList();
    }
}
