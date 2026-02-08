using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanTransactionTypes.Commands;

public sealed record CreateLoanTransactionTypeCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateLoanTransactionTypeCommandHandler : IRequestHandler<CreateLoanTransactionTypeCommand, ReferenceItemDto>
{
    private readonly ICrudRepository<LoanTransactionType> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanTransactionTypeCommandHandler(ICrudRepository<LoanTransactionType> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReferenceItemDto> Handle(CreateLoanTransactionTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new LoanTransactionType
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsActive = request.Request.IsActive
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
