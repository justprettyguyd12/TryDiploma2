namespace DataAccess.Context;

internal interface IContextFactory
{
    ApplicationContext CreateContext();
}