using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace DataAccess;

public class DataAccessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        var assembly = Assembly.Load(new AssemblyName("DotNetClub.Core"));
        foreach (var typeInfo in assembly.DefinedTypes)
        {
            if (typeInfo.Name.EndsWith("Service"))
            {
                builder.RegisterType(typeInfo.AsType());
            }
        }
    }
}