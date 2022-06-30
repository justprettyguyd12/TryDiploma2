using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TryDiploma.Data.Entities;

namespace TryDiploma;

public static class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}

public static class Databaseinitializer
{
    public static void Init(IServiceProvider scopeServiceProvider)
    {
        var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
        
        if (userManager == null)
            throw new Exception("Context doesn't exists");

        var user = new ApplicationUser()
        {
            UserName = "user"
        };

        var result = userManager.CreateAsync(user, "123").GetAwaiter().GetResult();
        if (result.Succeeded)
            userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Developer")).GetAwaiter().GetResult();
    }
}