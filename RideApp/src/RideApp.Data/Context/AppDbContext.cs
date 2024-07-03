using Microsoft.EntityFrameworkCore;
using RideApp.Domain.Entities;

namespace RideApp.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions){}
    
    public DbSet<Account> Accounts { get; init; }
    public DbSet<Ride> Rides { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.OwnsOne(e => e.Cpf)
                .Property(cpf => cpf.Value)
                .HasColumnName("Cpf");
        });

        base.OnModelCreating(modelBuilder);
    }
}