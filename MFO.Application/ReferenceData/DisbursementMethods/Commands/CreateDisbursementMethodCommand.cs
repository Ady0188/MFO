using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.DisbursementMethods.Commands;

public sealed record CreateDisbursementMethodCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateDisbursementMethodCommandHandler : IRequestHandler<CreateDisbursementMethodCommand, ReferenceItemDto>
{
    private readonly IAppDbContext _dbContext;

    public CreateDisbursementMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto> Handle(CreateDisbursementMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = new DisbursementMethod
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsActive = request.Request.IsActive
        };

        _dbContext.DisbursementMethods.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
