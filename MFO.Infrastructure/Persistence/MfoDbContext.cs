using MFO.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MFO.Infrastructure.Persistence;

internal sealed class MfoDbContext : IdentityDbContext<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole<Guid>, Guid>
{
    public MfoDbContext(DbContextOptions<MfoDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<LoanAccount> LoanAccounts => Set<LoanAccount>();
    public DbSet<LoanTransaction> LoanTransactions => Set<LoanTransaction>();
    public DbSet<LoanStatus> LoanStatuses => Set<LoanStatus>();
    public DbSet<LoanTransactionType> LoanTransactionTypes => Set<LoanTransactionType>();
    public DbSet<LoanProduct> LoanProducts => Set<LoanProduct>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<PaymentFrequency> PaymentFrequencies => Set<PaymentFrequency>();
    public DbSet<DisbursementMethod> DisbursementMethods => Set<DisbursementMethod>();
    public DbSet<RepaymentMethod> RepaymentMethods => Set<RepaymentMethod>();
    public DbSet<Purpose> Purposes => Set<Purpose>();
    public DbSet<PenaltyPolicy> PenaltyPolicies => Set<PenaltyPolicy>();
    public DbSet<CustomerStatus> CustomerStatuses => Set<CustomerStatus>();
    public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Customer>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.NationalId).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PhoneNumber).HasMaxLength(30).IsRequired();
            entity.HasOne(x => x.CustomerType)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CustomerTypeId);
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
            entity.HasOne(x => x.Branch)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BranchId);
            entity.HasOne(x => x.Curator)
                .WithMany(x => x.CuratedLoans)
                .HasForeignKey(x => x.CuratorId);
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

        builder.Entity<LoanTransactionType>(entity =>
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

        builder.Entity<CustomerType>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<Currency>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(3).IsRequired();
            entity.Property(x => x.NumericCode).HasMaxLength(3).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Symbol).HasMaxLength(10);
        });

        builder.Entity<Branch>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Code).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
        });

        builder.Entity<Employee>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.HasOne(x => x.Branch)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.BranchId);
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

        builder.Entity<LoanAccount>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.AccountNumber).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Balance).HasPrecision(18, 2);
            entity.HasOne(x => x.Loan)
                .WithMany()
                .HasForeignKey(x => x.LoanId);
        });

        builder.Entity<LoanTransaction>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Amount).HasPrecision(18, 2);
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasOne(x => x.Loan)
                .WithMany()
                .HasForeignKey(x => x.LoanId);
            entity.HasOne(x => x.LoanAccount)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.LoanAccountId);
            entity.HasOne(x => x.TransactionType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TransactionTypeId);
        });

        var loanStatusDraftId = Guid.Parse("6b5a3f41-2f04-4eb6-a4e8-1c4f55b2a6f1");
        var loanStatusPendingId = Guid.Parse("c72b1c34-2b5d-45ad-9c3c-1c8b6d9c0f2a");
        var loanStatusApprovedId = Guid.Parse("8b9f77f8-9f0d-4f5c-9a49-3d1dfc55f9d7");
        var loanStatusDisbursedId = Guid.Parse("14cf2ac0-3d2d-4f35-9b64-8d9ef0d2a343");
        var loanStatusActiveId = Guid.Parse("2eb7d12d-1a2c-4e2d-8bbf-ff18c89c41f3");
        var loanStatusOverdueId = Guid.Parse("f2e57b5b-f2be-4fb7-9a4e-4bb2b57c6a2a");
        var loanStatusRestructuredId = Guid.Parse("b93efb6a-1249-4b5b-9a4c-5c7a3bb4d3ae");
        var loanStatusProlongedId = Guid.Parse("6e0f0f4e-6d4b-4d18-9ed9-5d6a6f2bc71f");
        var loanStatusClosedId = Guid.Parse("d2a3547d-0b6c-4c17-9f73-7d8f2f4d6a1e");
        var loanStatusWrittenOffId = Guid.Parse("6b8949d8-1f89-4d2b-9d39-6d3b0e2d9fd2");
        var loanStatusCancelledId = Guid.Parse("f7c6ec3f-5b18-4fd1-9bb1-5f0c7c8d2a91");

        var customerStatusActiveId = Guid.Parse("4a2f9f2b-7f9f-41c4-9f3b-0c3d24b7c2a1");
        var customerStatusInactiveId = Guid.Parse("9d2f7a6c-221b-4e8a-8d1f-8af2d5a3a7a7");
        var customerStatusBlacklistedId = Guid.Parse("f1a7c4a9-5f1c-4e2a-9b4d-3f2a1d6c8e7a");

        var customerTypeIndividualId = Guid.Parse("7c6b2b1f-8a0c-4f2b-9a5b-4b1f7c2b5d3a");
        var customerTypeBusinessId = Guid.Parse("d1a9b5c3-2f4e-4b8d-8c2e-5f3a1b7c9d0e");

        var currencyTjsId = Guid.Parse("5d2c7a1f-8b9e-4c2a-9f1d-2b3c4d5e6f70");
        var currencyRubId = Guid.Parse("1c2d3e4f-5a6b-7c8d-9e0f-1a2b3c4d5e6f");
        var currencyUsdId = Guid.Parse("7a3d2f1c-9b4e-4a1b-8d2c-6f1a9d3b2c4d");
        var currencyEurId = Guid.Parse("b2c3d4e5-f6a7-48b9-0c1d-2e3f4a5b6c7d");

        var branchMainId = Guid.Parse("a22c6f48-1d5c-4d03-86b8-1f2f9b3a3e14");
        var branchBranchId = Guid.Parse("0d2d0d7d-7317-4f0e-a62a-6e7f8a3f7f3c");

        var employeeAdminId = Guid.Parse("e6a1f9a5-0a5b-4a4c-9d22-8a1f78e3e81f");
        var employeeCuratorId = Guid.Parse("c9c6dfd0-93b4-4d0e-86fa-3e7a4f6e5f2f");

        var transactionTypeDisbursementId = Guid.Parse("bf7b7a28-0e9a-4b5d-b9b2-2f4f6a6c0e3b");
        var transactionTypeRepaymentId = Guid.Parse("8b72e8f5-51a6-4c72-95c2-4ad5f6a7d8f1");
        var transactionTypeTopupId = Guid.Parse("7e2a4d8d-49f1-4f0b-9c2d-7b1f8f6a1e2c");
        var transactionTypeRestructureId = Guid.Parse("a1c4d7e9-3b5f-4b2c-9a3e-2d6f8b1c7e0a");
        var transactionTypeProlongationId = Guid.Parse("b2d5e8f1-4c6a-4d3b-8e1f-3c7a9d2e6f0b");
        var transactionTypePenaltyId = Guid.Parse("c3e6f9a2-5d7b-4e4c-9f2a-4d8b0e3f7a1c");
        var transactionTypeWriteOffId = Guid.Parse("d4f7a0b3-6e8c-4f5d-8a3b-5e9c1f4a8b2d");

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
            new LoanStatus { Id = loanStatusDraftId, Code = "DRAFT", Name = "Черновик", IsClosed = false },
            new LoanStatus { Id = loanStatusPendingId, Code = "PENDING", Name = "В ожидании", IsClosed = false },
            new LoanStatus { Id = loanStatusApprovedId, Code = "APPROVED", Name = "Одобрен", IsClosed = false },
            new LoanStatus { Id = loanStatusDisbursedId, Code = "DISBURSED", Name = "Выдан", IsClosed = false },
            new LoanStatus { Id = loanStatusActiveId, Code = "ACTIVE", Name = "Работает", IsClosed = false },
            new LoanStatus { Id = loanStatusOverdueId, Code = "OVERDUE", Name = "Просрочен", IsClosed = false },
            new LoanStatus { Id = loanStatusRestructuredId, Code = "RESTRUCTURED", Name = "Реструктурирован", IsClosed = false },
            new LoanStatus { Id = loanStatusProlongedId, Code = "PROLONGED", Name = "Пролонгирован", IsClosed = false },
            new LoanStatus { Id = loanStatusClosedId, Code = "CLOSED", Name = "Закрыт", IsClosed = true },
            new LoanStatus { Id = loanStatusWrittenOffId, Code = "WRITTEN_OFF", Name = "Списан", IsClosed = true },
            new LoanStatus { Id = loanStatusCancelledId, Code = "CANCELLED", Name = "Отменен", IsClosed = true }
        );
        builder.Entity<CustomerStatus>().HasData(
            new CustomerStatus { Id = customerStatusActiveId, Code = "ACTIVE", Name = "Активный", IsActive = true },
            new CustomerStatus { Id = customerStatusInactiveId, Code = "INACTIVE", Name = "Неактивный", IsActive = false },
            new CustomerStatus { Id = customerStatusBlacklistedId, Code = "BLACKLISTED", Name = "В черном списке", IsActive = false }
        );
        builder.Entity<CustomerType>().HasData(
            new CustomerType { Id = customerTypeIndividualId, Code = "INDIVIDUAL", Name = "Физическое лицо", IsActive = true },
            new CustomerType { Id = customerTypeBusinessId, Code = "BUSINESS", Name = "Юридическое лицо", IsActive = true }
        );
        builder.Entity<Currency>().HasData(
            new Currency { Id = currencyTjsId, Code = "TJS", NumericCode = "972", Name = "Таджикский сомони", Symbol = "TJS", IsActive = true },
            new Currency { Id = currencyRubId, Code = "RUB", NumericCode = "643", Name = "Российский рубль", Symbol = "RUB", IsActive = true },
            new Currency { Id = currencyUsdId, Code = "USD", NumericCode = "840", Name = "Доллар США", Symbol = "USD", IsActive = true },
            new Currency { Id = currencyEurId, Code = "EUR", NumericCode = "978", Name = "Евро", Symbol = "EUR", IsActive = true }
        );
        builder.Entity<Branch>().HasData(
            new Branch { Id = branchMainId, Code = "001", Name = "Головной офис", IsActive = true },
            new Branch { Id = branchBranchId, Code = "002", Name = "Филиал 2", IsActive = true }
        );
        builder.Entity<Employee>().HasData(
            new Employee { Id = employeeAdminId, FullName = "Администратор", BranchId = branchMainId, IsActive = true },
            new Employee { Id = employeeCuratorId, FullName = "Куратор", BranchId = branchMainId, IsActive = true }
        );
        builder.Entity<PaymentFrequency>().HasData(
            new PaymentFrequency { Id = frequencyWeeklyId, Code = "WEEKLY", Name = "Еженедельно", IntervalDays = 7, IsActive = true },
            new PaymentFrequency { Id = frequencyBiweeklyId, Code = "BIWEEKLY", Name = "Раз в две недели", IntervalDays = 14, IsActive = true },
            new PaymentFrequency { Id = frequencyMonthlyId, Code = "MONTHLY", Name = "Ежемесячно", IntervalDays = 30, IsActive = true }
        );
        builder.Entity<DisbursementMethod>().HasData(
            new DisbursementMethod { Id = disbursementCashId, Code = "CASH", Name = "Наличные", IsActive = true },
            new DisbursementMethod { Id = disbursementCardId, Code = "CARD", Name = "Карта", IsActive = true },
            new DisbursementMethod { Id = disbursementBankId, Code = "BANK_TRANSFER", Name = "Банковский перевод", IsActive = true }
        );
        builder.Entity<RepaymentMethod>().HasData(
            new RepaymentMethod { Id = repaymentCashId, Code = "CASH", Name = "Наличные", IsActive = true },
            new RepaymentMethod { Id = repaymentCardId, Code = "CARD", Name = "Карта", IsActive = true },
            new RepaymentMethod { Id = repaymentBankId, Code = "BANK_TRANSFER", Name = "Банковский перевод", IsActive = true },
            new RepaymentMethod { Id = repaymentOnlineId, Code = "ONLINE", Name = "Онлайн", IsActive = true }
        );
        builder.Entity<Purpose>().HasData(
            new Purpose { Id = purposePersonalId, Code = "PERSONAL", Name = "Личные нужды", IsActive = true },
            new Purpose { Id = purposeBusinessId, Code = "BUSINESS", Name = "Бизнес", IsActive = true },
            new Purpose { Id = purposeEducationId, Code = "EDUCATION", Name = "Образование", IsActive = true },
            new Purpose { Id = purposeMedicalId, Code = "MEDICAL", Name = "Медицина", IsActive = true },
            new Purpose { Id = purposeOtherId, Code = "OTHER", Name = "Другое", IsActive = true }
        );
        builder.Entity<PenaltyPolicy>().HasData(
            new PenaltyPolicy { Id = penaltyStandardId, Code = "STANDARD", Name = "Стандартный", PenaltyRate = 0.5m, FixedFee = 0m, IsActive = true },
            new PenaltyPolicy { Id = penaltyStrictId, Code = "STRICT", Name = "Строгий", PenaltyRate = 1.0m, FixedFee = 5m, IsActive = true },
            new PenaltyPolicy { Id = penaltySoftId, Code = "SOFT", Name = "Мягкий", PenaltyRate = 0.2m, FixedFee = 0m, IsActive = true }
        );
        builder.Entity<LoanTransactionType>().HasData(
            new LoanTransactionType { Id = transactionTypeDisbursementId, Code = "DISBURSEMENT", Name = "Выдача кредита", IsActive = true },
            new LoanTransactionType { Id = transactionTypeRepaymentId, Code = "REPAYMENT", Name = "Погашение кредита", IsActive = true },
            new LoanTransactionType { Id = transactionTypeTopupId, Code = "ACCOUNT_TOPUP", Name = "Пополнение счета", IsActive = true },
            new LoanTransactionType { Id = transactionTypeRestructureId, Code = "RESTRUCTURE", Name = "Реструктуризация", IsActive = true },
            new LoanTransactionType { Id = transactionTypeProlongationId, Code = "PROLONGATION", Name = "Пролонгация", IsActive = true },
            new LoanTransactionType { Id = transactionTypePenaltyId, Code = "PENALTY", Name = "Штраф", IsActive = true },
            new LoanTransactionType { Id = transactionTypeWriteOffId, Code = "WRITE_OFF", Name = "Списание", IsActive = true }
        );
        builder.Entity<LoanProduct>().HasData(
            new LoanProduct
            {
                Id = productShortTermId,
                Code = "SHORT_TERM",
                Name = "Краткосрочный кредит",
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
                Name = "Долгосрочный кредит",
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
        );}
}
