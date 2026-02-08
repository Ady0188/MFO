using MFO.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MFO.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Loan> Loans { get; }
    DbSet<LoanStatus> LoanStatuses { get; }
    DbSet<LoanProduct> LoanProducts { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<PaymentFrequency> PaymentFrequencies { get; }
    DbSet<DisbursementMethod> DisbursementMethods { get; }
    DbSet<RepaymentMethod> RepaymentMethods { get; }
    DbSet<Purpose> Purposes { get; }
    DbSet<PenaltyPolicy> PenaltyPolicies { get; }
    DbSet<CustomerStatus> CustomerStatuses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
