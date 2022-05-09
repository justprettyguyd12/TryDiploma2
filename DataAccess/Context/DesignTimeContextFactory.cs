using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Context;

internal sealed class DesignTimeContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args) => 
        new DatabaseContextFactory().CreateContext();
}