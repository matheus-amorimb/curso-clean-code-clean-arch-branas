using Microsoft.EntityFrameworkCore;
using RideApp.Domain.Entities;

namespace RideApp.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions){}
    
    public DbSet<Account> Accounts { get; init; }
}