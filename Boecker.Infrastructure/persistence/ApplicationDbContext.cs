using Boecker.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceService> InvoiceServices { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<InvoiceStatusLog> InvoiceStatusLogs { get; set; }
    public DbSet<Lookup> Lookups => Set<Lookup>();
    public DbSet<LookupItem> LookupItems => Set<LookupItem>();
    public DbSet<ServiceSchedule> ServiceSchedules => Set<ServiceSchedule>();
    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<FollowUpSchedule> FollowUpSchedules => Set<FollowUpSchedule>();
    public DbSet<Contract> Contracts => Set<Contract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Fix Decimal Precision
        modelBuilder.Entity<Invoice>()
            .Property(i => i.TotalBeforeTax)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.VATPercentage)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.VATAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.TotalAfterTax)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.RecurrencePeriod)
            .HasConversion<string>();

        modelBuilder.Entity<Invoice>()
            .Property(i => i.IsRecurring)
            .HasDefaultValue(false);

        modelBuilder.Entity<Service>()
            .Property(s => s.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<InvoiceService>()
            .Property(isv => isv.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<InvoiceService>()
            .Property(s => s.CompletionDate)
            .IsRequired(false);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.Status)
            .HasConversion<string>();

        modelBuilder.Entity<InvoiceStatusLog>()
            .Property(x => x.OldStatus)
            .HasConversion<string>();

        modelBuilder.Entity<InvoiceStatusLog>()
            .Property(x => x.NewStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Contract>()
            .Property(c => c.Status)
            .HasConversion<string>();

        // ❗ FIX: Prevent multiple cascade delete paths involving Contract

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Contract)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.ContractId)
            .OnDelete(DeleteBehavior.Restrict); // ✅ Safe

        modelBuilder.Entity<ServiceSchedule>()
            .HasOne(s => s.Contract)
            .WithMany(c => c.ServiceSchedules)
            .HasForeignKey(s => s.ContractId)
            .OnDelete(DeleteBehavior.Cascade); // or Restrict if needed

        modelBuilder.Entity<FollowUpSchedule>()
            .HasOne(f => f.Contract)
            .WithMany(c => c.FollowUps)
            .HasForeignKey(f => f.ContractId)
            .OnDelete(DeleteBehavior.Restrict); // ✅ To avoid multiple cascade paths
    }
}
