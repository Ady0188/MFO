using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record CreateLoanStatusCommand(LoanStatusRequest Request) : IRequest<LoanStatusDto>;

public sealed class CreateLoanStatusCommandHandler : IRequestHandler<CreateLoanStatusCommand, LoanStatusDto>
{
    private readonly ICrudRepository<LoanStatus> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanStatusCommandHandler(ICrudRepository<LoanStatus> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoanStatusDto> Handle(CreateLoanStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new LoanStatus
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsClosed = request.Request.IsClosed
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoanStatusDto(entity.Id, entity.Code, entity.Name, entity.IsClosed);
    }
}
