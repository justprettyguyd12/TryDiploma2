using AutoMapper;
using DataAccess.Context;

namespace DataAccess.Services;

public abstract class DbService
{
    private protected readonly ApplicationContext Context;
    private protected readonly IMapper Mapper;

    private protected DbService()
    {
        var config = new MapperConfiguration(cfg
            => cfg.AddProfile<AutoMapperProfile>());
        config.AssertConfigurationIsValid();
        Mapper = new Mapper(config);
        
        Context = new DatabaseContextFactory().CreateContext();
    }
    
    private protected void Transaction(Action action)
    {
        using var transaction = Context.Database.BeginTransaction();

        action();
        
        Context.SaveChanges();
        transaction.Commit();
    }
}