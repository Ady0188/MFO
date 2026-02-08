using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record CreateLoanStatusCommand(LoanStatusRequest Request) : IRequest<LoanStatusDto>;

public sealed class CreateLoanStatusCommandHandler : IRequestHandler<CreateLoanStatusCommand, LoanStatusDto>
{
    private readonly IAppDbContext _dbContext;

    public CreateLoanStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
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

        _dbContext.LoanStatuses.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LoanStatusDto(entity.Id, entity.Code, entity.Name, entity.IsClosed);
    }
}
