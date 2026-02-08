using MediatR;
using Microsoft.EntityFrameworkCore;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Commands;

public sealed record CreateLoanProductCommand(LoanProductRequest Request) : IRequest<CommandResult<LoanProductDto>>;

public sealed class CreateLoanProductCommandHandler : IRequestHandler<CreateLoanProductCommand, CommandResult<LoanProductDto>>
{
    private readonly IAppDbContext _dbContext;

    public CreateLoanProductCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CommandResult<LoanProductDto>> Handle(CreateLoanProductCommand request, CancellationToken cancellationToken)
    {
        var missing = await GetMissingReferencesAsync(request.Request, cancellationToken);
        if (missing.Count > 0)
        {
            return CommandResult<LoanProductDto>.Missing(missing);
        }

        var entity = new LoanProduct
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code,
            Name = request.Request.Name,
            InterestRate = request.Request.InterestRate,
            OriginationFee = request.Request.OriginationFee,
            MinAmount = request.Request.MinAmount,
            MaxAmount = request.Request.MaxAmount,
            MinTermMonths = request.Request.MinTermMonths,
            MaxTermMonths = request.Request.MaxTermMonths,
            CurrencyId = request.Request.CurrencyId,
            PaymentFrequencyId = request.Request.PaymentFrequencyId,
            PenaltyPolicyId = request.Request.PenaltyPolicyId,
            IsActive = request.Request.IsActive
        };

        _dbContext.LoanProducts.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CommandResult<LoanProductDto>.Success(new LoanProductDto(
            entity.Id,
            entity.Code,
            entity.Name,
            entity.InterestRate,
            entity.OriginationFee,
            entity.MinAmount,
            entity.MaxAmount,
            entity.MinTermMonths,
            entity.MaxTermMonths,
            entity.CurrencyId,
            entity.PaymentFrequencyId,
            entity.PenaltyPolicyId,
            entity.IsActive));
    }

    private async Task<List<string>> GetMissingReferencesAsync(LoanProductRequest request, CancellationToken cancellationToken)
    {
        var missing = new List<string>();

        if (!await _dbContext.Currencies.AnyAsync(x => x.Id == request.CurrencyId, cancellationToken))
        {
            missing.Add("Currency");
        }

        if (!await _dbContext.PaymentFrequencies.AnyAsync(x => x.Id == request.PaymentFrequencyId, cancellationToken))
        {
            missing.Add("PaymentFrequency");
        }

        if (!await _dbContext.PenaltyPolicies.AnyAsync(x => x.Id == request.PenaltyPolicyId, cancellationToken))
        {
            missing.Add("PenaltyPolicy");
        }

        return missing;
    }
}
