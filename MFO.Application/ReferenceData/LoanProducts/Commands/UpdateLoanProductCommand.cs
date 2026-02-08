using MediatR;
using MFO.Application.Common.Interfaces;
using MFO.Application.Common.Models;
using MFO.Domain.Entities;

namespace MFO.Application.ReferenceData.LoanProducts.Commands;

public sealed record UpdateLoanProductCommand(Guid Id, LoanProductRequest Request) : IRequest<CommandResult<LoanProductDto>>;

public sealed class UpdateLoanProductCommandHandler : IRequestHandler<UpdateLoanProductCommand, CommandResult<LoanProductDto>>
{
    private readonly ICrudRepository<LoanProduct> _repository;
    private readonly IReferenceDataLookupRepository _referenceLookup;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLoanProductCommandHandler(
        ICrudRepository<LoanProduct> repository,
        IReferenceDataLookupRepository referenceLookup,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _referenceLookup = referenceLookup;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<LoanProductDto>> Handle(UpdateLoanProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return CommandResult<LoanProductDto>.NotFound();
        }

        var missing = await GetMissingReferencesAsync(request.Request, cancellationToken);
        if (missing.Count > 0)
        {
            return CommandResult<LoanProductDto>.Missing(missing);
        }

        entity.Code = request.Request.Code;
        entity.Name = request.Request.Name;
        entity.InterestRate = request.Request.InterestRate;
        entity.OriginationFee = request.Request.OriginationFee;
        entity.MinAmount = request.Request.MinAmount;
        entity.MaxAmount = request.Request.MaxAmount;
        entity.MinTermMonths = request.Request.MinTermMonths;
        entity.MaxTermMonths = request.Request.MaxTermMonths;
        entity.CurrencyId = request.Request.CurrencyId;
        entity.PaymentFrequencyId = request.Request.PaymentFrequencyId;
        entity.PenaltyPolicyId = request.Request.PenaltyPolicyId;
        entity.IsActive = request.Request.IsActive;

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
