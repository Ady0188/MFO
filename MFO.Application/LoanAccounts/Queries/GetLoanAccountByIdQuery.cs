using MediatR;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.LoanAccounts.Queries;

public sealed record GetLoanAccountByIdQuery(Guid Id) : IRequest<LoanAccountDto?>;

public sealed class GetLoanAccountByIdQueryHandler : IRequestHandler<GetLoanAccountByIdQuery, LoanAccountDto?>
{
    private readonly ILoanAccountRepository _repository;

    public GetLoanAccountByIdQueryHandler(ILoanAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoanAccountDto?> Handle(GetLoanAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
        return item is null ? null : new LoanAccountDto(item.Id, item.LoanId, item.AccountNumber, item.Balance, item.OpenedOn, item.ClosedOn, item.IsActive);
    }
}
