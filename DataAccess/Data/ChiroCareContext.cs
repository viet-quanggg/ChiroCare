using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class ChiroCareContext : DbContext

{
    public ChiroCareContext()
    {
    }
    
    public ChiroCareContext(DbContextOptions<ChiroCareContext> options) : base(options)
    {
    }
    
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Server=tcp:chirocare-db.database.windows.net,1433;Initial Catalog=chiroDB;Persist Security Info=False;User ID=chirocaredb;Password=123qwe!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId);
            entity.Property(e => e.SessionInfo).IsRequired().HasMaxLength(1000).HasColumnName("SessionInfo"); // Example: Setting column name explicitly
            entity.Property(e => e.SessionDate).HasColumnType("datetime"); // Example: Setting column data type
            entity.HasOne(e => e.Patient).WithMany(u => u.UserSessions).HasForeignKey(e => e.PatientId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Therapist).WithMany().HasForeignKey(e => e.TherapistId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.PhoneNumber).IsRequired();
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Dob).HasColumnType("date"); // Example: Setting column data type
            // Configure relationships with other entities
            entity.HasMany(u => u.UserSessions).WithOne(s => s.Patient).HasForeignKey(s => s.PatientId).OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(u => u.UserInvoices).WithOne(i => i.Patient).HasForeignKey(i => i.PatientId).OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(u => u.UserWorkShifts).WithOne(s => s.ShiftUser).HasForeignKey(s => s.ShiftId).OnDelete(DeleteBehavior.NoAction);
            // Define other relationships as needed, e.g., UserWorkShifts
        });
        
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);
            entity.Property(e => e.CreateDate).IsRequired().HasColumnType("datetime");
            entity.Property(e => e.InvoiceDiagnose).HasMaxLength(1000); // Example: Setting max length for InvoiceDiagnose
            entity.Property(e => e.InvoiceDescription).IsRequired();
            entity.HasOne(e => e.Patient).WithMany().HasForeignKey(e => e.PatientId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(e => e.ListSessions).WithOne().HasForeignKey("InvoiceId").IsRequired().OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId);
            entity.Property(e => e.ServiceName).IsRequired();
            entity.Property(e => e.ServicePrice).HasColumnType("decimal(18,2)"); // Example: Setting precision and scale for ServicePrice
            entity.HasOne(e => e.ServiceCategory).WithMany().HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<WorkShift>(entity =>
        {
            entity.HasKey(e => e.ShiftId);
            entity.Property(e => e.StartDateTime).IsRequired().HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).IsRequired().HasColumnType("datetime");
            entity.Property(e => e.ShiftNote).HasMaxLength(1000); // Example: Setting max length for ShiftNote

            // Configure relationship with User entity (Therapist)
            entity.HasOne(ws => ws.ShiftUser)
                .WithMany(u => u.UserWorkShifts)
                .HasForeignKey(ws => ws.TherapistId)
                .IsRequired().OnDelete(DeleteBehavior.NoAction);
        });
        
       
    }
}