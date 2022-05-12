using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TryDiploma.Data.Entities;

namespace TryDiploma.Data;

public class UsersContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }
}