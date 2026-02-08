using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Commands;

public sealed record CreateLoanProductCommand(LoanProductRequest Request) : IRequest<CommandResult<LoanProductDto>>;

public sealed class CreateLoanProductCommandHandler : IRequestHandler<CreateLoanProductCommand, CommandResult<LoanProductDto>>
{
    private readonly ICrudRepository<LoanProduct> _repository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanProductCommandHandler(
        ICrudRepository<LoanProduct> repository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
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

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        if (!await _referenceLookup.CurrencyExistsAsync(request.CurrencyId, cancellationToken))
        {
            missing.Add("Currency");
        }

        if (!await _referenceLookup.PaymentFrequencyExistsAsync(request.PaymentFrequencyId, cancellationToken))
        {
            missing.Add("PaymentFrequency");
        }

        if (!await _referenceLookup.PenaltyPolicyExistsAsync(request.PenaltyPolicyId, cancellationToken))
        {
            missing.Add("PenaltyPolicy");
        }

        return missing;
    }
}
