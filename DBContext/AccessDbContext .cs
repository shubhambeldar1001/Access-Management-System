using Access_Management_System.Model;
using Microsoft.EntityFrameworkCore;

public class AccessDbContext : DbContext
{
    public AccessDbContext(DbContextOptions<AccessDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetAprover> AssetApprovers { get; set; }
    public DbSet<AccessRequest> AccessRequests { get; set; }
    public DbSet<AccessApproval> AccessApprovals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>()
            .HasMany(a => a.AssetUsers)
            .WithMany(u => u.AssetsWithAccess);

        modelBuilder.Entity<AssetAprover>()
            .HasOne(a => a.Approver)
            .WithMany()
            .HasForeignKey(a => a.ApproverId);

        modelBuilder.Entity<AccessApproval>()
            .HasIndex(a => new { a.AccessRequestId, a.ApproverId }).IsUnique();
    }
}
