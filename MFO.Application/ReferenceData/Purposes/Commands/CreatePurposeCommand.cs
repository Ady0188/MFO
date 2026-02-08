using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.Purposes.Commands;

public sealed record CreatePurposeCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreatePurposeCommandHandler : IRequestHandler<CreatePurposeCommand, ReferenceItemDto>
{
    private readonly IAppDbContext _dbContext;

    public CreatePurposeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto> Handle(CreatePurposeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Purpose
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsActive = request.Request.IsActive
        };

        _dbContext.Purposes.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
