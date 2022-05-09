using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

internal class DatabaseContextFactory : IContextFactory
{
    public ApplicationContext CreateContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder
            .UseNpgsql("Host=localhost;Port=5432;Database=beatmaker;Username=postgres;Password=123")
            .EnableSensitiveDataLogging();
        return new ApplicationContext(optionsBuilder.Options);
    }
}