using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record UpdateLoanStatusCommand(Guid Id, LoanStatusRequest Request) : IRequest<LoanStatusDto?>;

public sealed class UpdateLoanStatusCommandHandler : IRequestHandler<UpdateLoanStatusCommand, LoanStatusDto?>
{
    private readonly ICrudRepository<LoanStatus> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLoanStatusCommandHandler(ICrudRepository<LoanStatus> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoanStatusDto?> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IsClosed = request.Request.IsClosed;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoanStatusDto(entity.Id, entity.Code, entity.Name, entity.IsClosed);
    }
}
