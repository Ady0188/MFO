using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanTransactionTypes.Queries;

public sealed record GetLoanTransactionTypesQuery : IRequest<IReadOnlyList<ReferenceItemDto>>;

public sealed class GetLoanTransactionTypesQueryHandler : IRequestHandler<GetLoanTransactionTypesQuery, IReadOnlyList<ReferenceItemDto>>
{
    private readonly ICrudRepository<LoanTransactionType> _repository;

    public GetLoanTransactionTypesQueryHandler(ICrudRepository<LoanTransactionType> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReferenceItemDto>> Handle(GetLoanTransactionTypesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new ReferenceItemDto(x.Id, x.Code, x.Name, x.IsActive)).ToList();
    }
}
