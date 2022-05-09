using DataAccess.Models;
using DataAccess.Models.Crm;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

internal sealed class ApplicationContext : DbContext
{
    public DbSet<FunnelEntity> Funnels { get; set; }
    public DbSet<SectionEntity> Sections { get; set; }
    public DbSet<BeatEntity> Beats { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ContractEntity> Contracts { get; set; }
    public DbSet<DealEntity> Deals { get; set; }
    public DbSet<ShoppingBagEntity> Bags { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}