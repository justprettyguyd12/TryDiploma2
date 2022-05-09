using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    internal class InMemoryContextFactory : IContextFactory
    {
        public ApplicationContext CreateContext()
        { 
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}