using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;

namespace MFO.Application.ReferenceData.LoanStatuses.Commands;

public sealed record UpdateLoanStatusCommand(Guid Id, LoanStatusRequest Request) : IRequest<LoanStatusDto?>;

public sealed class UpdateLoanStatusCommandHandler : IRequestHandler<UpdateLoanStatusCommand, LoanStatusDto?>
{
    private readonly IAppDbContext _dbContext;

    public UpdateLoanStatusCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoanStatusDto?> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.LoanStatuses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.IsClosed = request.Request.IsClosed;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LoanStatusDto(entity.Id, entity.Code, entity.Name, entity.IsClosed);
    }
}
