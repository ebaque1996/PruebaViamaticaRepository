using Microsoft.EntityFrameworkCore;
using Viamatica.Domain.Entities;

namespace Viamatica.Infrastructure.Persistence;

public class ViamaticaDbContext : DbContext
{
    public ViamaticaDbContext(DbContextOptions<ViamaticaDbContext> options) : base(options)
    {
    }

    public DbSet<UserStatus> UserStatuses { get; set; } = null!;
    public DbSet<Rol> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Cash> Cashes { get; set; } = null!;
    public DbSet<UserCash> UserCashes { get; set; } = null!;
    public DbSet<Turn> Turns { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<StatusContract> StatusContracts { get; set; } = null!;
    public DbSet<MethodPayment> MethodPayments { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<AttentionType> AttentionTypes { get; set; } = null!;
    public DbSet<AttentionStatus> AttentionStatuses { get; set; } = null!;
    public DbSet<Attention> Attentions { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<RoleMenu> RoleMenus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserCash>()
            .HasKey(uc => new { uc.UserUserId, uc.CashCashId });

        modelBuilder.Entity<RoleMenu>()
            .HasKey(rm => new { rm.RolId, rm.MenuId });

        modelBuilder.Entity<Attention>()
            .HasOne(a => a.Turn)
            .WithOne(t => t.Attention)
            .HasForeignKey<Attention>(a => a.TurnTurnId);

        modelBuilder.Entity<Service>()
            .Property(s => s.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Service>()
            .Property(s => s.SpeedMbps)
            .HasColumnType("decimal(18,2)");

        //Primary keys
        modelBuilder.Entity<AttentionStatus>()
            .HasKey(a => a.StatusId);

        modelBuilder.Entity<UserStatus>()
            .HasKey(u => u.StatusId);

        modelBuilder.Entity<StatusContract>()
            .HasKey(s => s.StatusId);
    }
}