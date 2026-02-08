using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.RepaymentMethods.Commands;

public sealed record CreateRepaymentMethodCommand(ReferenceItemRequest Request) : IRequest<ReferenceItemDto>;

public sealed class CreateRepaymentMethodCommandHandler : IRequestHandler<CreateRepaymentMethodCommand, ReferenceItemDto>
{
    private readonly IAppDbContext _dbContext;

    public CreateRepaymentMethodCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReferenceItemDto> Handle(CreateRepaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = new RepaymentMethod
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            IsActive = request.Request.IsActive
        };

        _dbContext.RepaymentMethods.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ReferenceItemDto(entity.Id, entity.Code, entity.Name, entity.IsActive);
    }
}
