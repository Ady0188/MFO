using MFO.Application.Common.Interfaces;
using MFO.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MFO.Infrastructure.Persistence;

public sealed class MfoDbContext : IdentityDbContext<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole<Guid>, Guid>, IAppDbContext
{
    public MfoDbContext(DbContextOptions<MfoDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<LoanStatus> LoanStatuses => Set<LoanStatus>();
    public DbSet<LoanProduct> LoanProducts => Set<LoanProduct>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<PaymentFrequency> PaymentFrequencies => Set<PaymentFrequency>();
    public DbSet<DisbursementMethod> DisbursementMethods => Set<DisbursementMethod>();
    public DbSet<RepaymentMethod> RepaymentMethods => Set<RepaymentMethod>();
    public DbSet<Purpose> Purposes => Set<Purpose>();
    public DbSet<PenaltyPolicy> PenaltyPolicies => Set<PenaltyPolicy>();
    public DbSet<CustomerStatus> CustomerStatuses => Set<CustomerStatus>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Customer>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.NationalId).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PhoneNumber).HasMaxLength(30).IsRequired();
            entity.HasOne(x => x.Status)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.StatusId);
        });

        builder.Entity<Loan>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.LoanNumber).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PrincipalAmount).HasPrecision(18, 2);
            entity.Property(x => x.InterestRate).HasPrecision(7, 4);
            entity.Property(x => x.FeesAmount).HasPrecision(18, 2);
            entity.Property(x => x.PenaltyRate).HasPrecision(7, 4);
            entity.Property(x => x.TotalPayable).HasPrecision(18, 2);
            entity.Property(x => x.OutstandingPrincipal).HasPrecision(18, 2);
            entity.Property(x => x.OutstandingInterest).HasPrecision(18, 2);
            entity.HasOne(x => x.Customer)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.CustomerId);
            entity.HasOne(x => x.Product)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.ProductId);
            entity.HasOne(x => x.Status)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.StatusId);
            entity.HasOne(x => x.Currency)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.CurrencyId);
            entity.HasOne(x => x.DisbursementMethod)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.DisbursementMethodId);
            entity.HasOne(x => x.RepaymentMethod)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.RepaymentMethodId);
            entity.HasOne(x => x.Purpose)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.PurposeId);
            entity.HasOne(x => x.PaymentFrequency)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.PaymentFrequencyId);
            entity.HasOne(x => x.PenaltyPolicy)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.PenaltyPolicyId);
        });

        builder.Entity<LoanStatus>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<CustomerStatus>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<Currency>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(3).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Symbol).HasMaxLength(10);
        });

        builder.Entity<PaymentFrequency>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<DisbursementMethod>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<RepaymentMethod>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<Purpose>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<PenaltyPolicy>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.PenaltyRate).HasPrecision(7, 4);
            entity.Property(x => x.FixedFee).HasPrecision(18, 2);
        });

        builder.Entity<LoanProduct>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.InterestRate).HasPrecision(7, 4);
            entity.Property(x => x.OriginationFee).HasPrecision(18, 2);
            entity.Property(x => x.MinAmount).HasPrecision(18, 2);
            entity.Property(x => x.MaxAmount).HasPrecision(18, 2);
            entity.HasOne(x => x.Currency)
                .WithMany(x => x.LoanProducts)
                .HasForeignKey(x => x.CurrencyId);
            entity.HasOne(x => x.PaymentFrequency)
                .WithMany(x => x.LoanProducts)
                .HasForeignKey(x => x.PaymentFrequencyId);
            entity.HasOne(x => x.PenaltyPolicy)
                .WithMany(x => x.LoanProducts)
                .HasForeignKey(x => x.PenaltyPolicyId);
        });

        var loanStatusDraftId = Guid.Parse("6b5a3f41-2f04-4eb6-a4e8-1c4f55b2a6f1");
        var loanStatusActiveId = Guid.Parse("2eb7d12d-1a2c-4e2d-8bbf-ff18c89c41f3");
        var loanStatusOverdueId = Guid.Parse("f2e57b5b-f2be-4fb7-9a4e-4bb2b57c6a2a");
        var loanStatusClosedId = Guid.Parse("d2a3547d-0b6c-4c17-9f73-7d8f2f4d6a1e");
        var loanStatusWrittenOffId = Guid.Parse("6b8949d8-1f89-4d2b-9d39-6d3b0e2d9fd2");
        var loanStatusCancelledId = Guid.Parse("f7c6ec3f-5b18-4fd1-9bb1-5f0c7c8d2a91");

        var customerStatusActiveId = Guid.Parse("4a2f9f2b-7f9f-41c4-9f3b-0c3d24b7c2a1");
        var customerStatusInactiveId = Guid.Parse("9d2f7a6c-221b-4e8a-8d1f-8af2d5a3a7a7");
        var customerStatusBlacklistedId = Guid.Parse("f1a7c4a9-5f1c-4e2a-9b4d-3f2a1d6c8e7a");

        var currencyUsdId = Guid.Parse("7a3d2f1c-9b4e-4a1b-8d2c-6f1a9d3b2c4d");
        var currencyKztId = Guid.Parse("5d2c7a1f-8b9e-4c2a-9f1d-2b3c4d5e6f70");
        var currencyRubId = Guid.Parse("1c2d3e4f-5a6b-7c8d-9e0f-1a2b3c4d5e6f");

        var frequencyWeeklyId = Guid.Parse("12b4c5d6-7e8f-49a0-b1c2-3d4e5f6a7b8c");
        var frequencyBiweeklyId = Guid.Parse("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c");
        var frequencyMonthlyId = Guid.Parse("b7c8d9e0-f1a2-43b4-85c6-d7e8f9a0b1c2");

        var disbursementCashId = Guid.Parse("9a0b1c2d-3e4f-45a6-b7c8-d9e0f1a2b3c4");
        var disbursementCardId = Guid.Parse("c4b3a2f1-0e9d-48c7-b6a5-4f3e2d1c0b9a");
        var disbursementBankId = Guid.Parse("f0e9d8c7-b6a5-44f3-a2b1-c0d9e8f7a6b5");

        var repaymentCashId = Guid.Parse("3f2e1d0c-9b8a-47c6-b5a4-3f2e1d0c9b8a");
        var repaymentCardId = Guid.Parse("8a9b0c1d-2e3f-4a5b-8c9d-0e1f2a3b4c5d");
        var repaymentBankId = Guid.Parse("4c5b6a7d-8e9f-40a1-b2c3-d4e5f6a7b8c9");
        var repaymentOnlineId = Guid.Parse("e1f2a3b4-c5d6-47e8-9f0a-1b2c3d4e5f6a");

        var purposePersonalId = Guid.Parse("0a1b2c3d-4e5f-46a7-8b9c-0d1e2f3a4b5c");
        var purposeBusinessId = Guid.Parse("1b2c3d4e-5f6a-47b8-9c0d-1e2f3a4b5c6d");
        var purposeEducationId = Guid.Parse("2c3d4e5f-6a7b-48c9-0d1e-2f3a4b5c6d7e");
        var purposeMedicalId = Guid.Parse("3d4e5f6a-7b8c-49d0-1e2f-3a4b5c6d7e8f");
        var purposeOtherId = Guid.Parse("4e5f6a7b-8c9d-40e1-2f3a-4b5c6d7e8f9a");

        var penaltyStandardId = Guid.Parse("5f6a7b8c-9d0e-41f2-3a4b-5c6d7e8f9a0b");
        var penaltyStrictId = Guid.Parse("6a7b8c9d-0e1f-42a3-4b5c-6d7e8f9a0b1c");
        var penaltySoftId = Guid.Parse("7b8c9d0e-1f2a-43b4-5c6d-7e8f9a0b1c2d");

        var productShortTermId = Guid.Parse("8c9d0e1f-2a3b-44c5-6d7e-8f9a0b1c2d3e");
        var productLongTermId = Guid.Parse("9d0e1f2a-3b4c-45d6-7e8f-9a0b1c2d3e4f");

        builder.Entity<LoanStatus>().HasData(
            new LoanStatus { Id = loanStatusDraftId, Code = "DRAFT", Name = "Draft", IsClosed = false },
            new LoanStatus { Id = loanStatusActiveId, Code = "ACTIVE", Name = "Active", IsClosed = false },
            new LoanStatus { Id = loanStatusOverdueId, Code = "OVERDUE", Name = "Overdue", IsClosed = false },
            new LoanStatus { Id = loanStatusClosedId, Code = "CLOSED", Name = "Closed", IsClosed = true },
            new LoanStatus { Id = loanStatusWrittenOffId, Code = "WRITTEN_OFF", Name = "Written Off", IsClosed = true },
            new LoanStatus { Id = loanStatusCancelledId, Code = "CANCELLED", Name = "Cancelled", IsClosed = true }
        );

        builder.Entity<CustomerStatus>().HasData(
            new CustomerStatus { Id = customerStatusActiveId, Code = "ACTIVE", Name = "Active", IsActive = true },
            new CustomerStatus { Id = customerStatusInactiveId, Code = "INACTIVE", Name = "Inactive", IsActive = false },
            new CustomerStatus { Id = customerStatusBlacklistedId, Code = "BLACKLISTED", Name = "Blacklisted", IsActive = false }
        );

        builder.Entity<Currency>().HasData(
            new Currency { Id = currencyUsdId, Code = "USD", Name = "US Dollar", Symbol = "$", IsActive = true },
            new Currency { Id = currencyKztId, Code = "KZT", Name = "Kazakhstani Tenge", Symbol = "T", IsActive = true },
            new Currency { Id = currencyRubId, Code = "RUB", Name = "Russian Ruble", Symbol = "R", IsActive = true }
        );

        builder.Entity<PaymentFrequency>().HasData(
            new PaymentFrequency { Id = frequencyWeeklyId, Code = "WEEKLY", Name = "Weekly", IntervalDays = 7, IsActive = true },
            new PaymentFrequency { Id = frequencyBiweeklyId, Code = "BIWEEKLY", Name = "Biweekly", IntervalDays = 14, IsActive = true },
            new PaymentFrequency { Id = frequencyMonthlyId, Code = "MONTHLY", Name = "Monthly", IntervalDays = 30, IsActive = true }
        );

        builder.Entity<DisbursementMethod>().HasData(
            new DisbursementMethod { Id = disbursementCashId, Code = "CASH", Name = "Cash", IsActive = true },
            new DisbursementMethod { Id = disbursementCardId, Code = "CARD", Name = "Card", IsActive = true },
            new DisbursementMethod { Id = disbursementBankId, Code = "BANK_TRANSFER", Name = "Bank Transfer", IsActive = true }
        );

        builder.Entity<RepaymentMethod>().HasData(
            new RepaymentMethod { Id = repaymentCashId, Code = "CASH", Name = "Cash", IsActive = true },
            new RepaymentMethod { Id = repaymentCardId, Code = "CARD", Name = "Card", IsActive = true },
            new RepaymentMethod { Id = repaymentBankId, Code = "BANK_TRANSFER", Name = "Bank Transfer", IsActive = true },
            new RepaymentMethod { Id = repaymentOnlineId, Code = "ONLINE", Name = "Online", IsActive = true }
        );

        builder.Entity<Purpose>().HasData(
            new Purpose { Id = purposePersonalId, Code = "PERSONAL", Name = "Personal", IsActive = true },
            new Purpose { Id = purposeBusinessId, Code = "BUSINESS", Name = "Business", IsActive = true },
            new Purpose { Id = purposeEducationId, Code = "EDUCATION", Name = "Education", IsActive = true },
            new Purpose { Id = purposeMedicalId, Code = "MEDICAL", Name = "Medical", IsActive = true },
            new Purpose { Id = purposeOtherId, Code = "OTHER", Name = "Other", IsActive = true }
        );

        builder.Entity<PenaltyPolicy>().HasData(
            new PenaltyPolicy { Id = penaltyStandardId, Code = "STANDARD", Name = "Standard", PenaltyRate = 0.5m, FixedFee = 0m, IsActive = true },
            new PenaltyPolicy { Id = penaltyStrictId, Code = "STRICT", Name = "Strict", PenaltyRate = 1.0m, FixedFee = 5m, IsActive = true },
            new PenaltyPolicy { Id = penaltySoftId, Code = "SOFT", Name = "Soft", PenaltyRate = 0.2m, FixedFee = 0m, IsActive = true }
        );

        builder.Entity<LoanProduct>().HasData(
            new LoanProduct
            {
                Id = productShortTermId,
                Code = "SHORT_TERM",
                Name = "Short Term Loan",
                InterestRate = 2.5m,
                OriginationFee = 5m,
                MinAmount = 50m,
                MaxAmount = 500m,
                MinTermMonths = 1,
                MaxTermMonths = 3,
                CurrencyId = currencyUsdId,
                PaymentFrequencyId = frequencyWeeklyId,
                PenaltyPolicyId = penaltyStandardId,
                IsActive = true
            },
            new LoanProduct
            {
                Id = productLongTermId,
                Code = "LONG_TERM",
                Name = "Long Term Loan",
                InterestRate = 1.5m,
                OriginationFee = 10m,
                MinAmount = 300m,
                MaxAmount = 2000m,
                MinTermMonths = 6,
                MaxTermMonths = 24,
                CurrencyId = currencyUsdId,
                PaymentFrequencyId = frequencyMonthlyId,
                PenaltyPolicyId = penaltyStrictId,
                IsActive = true
            }
        );
    }
}
