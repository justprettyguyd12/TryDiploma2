using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace TryDiploma.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [ForeignKey(nameof(Client))]
    public Guid? ClientId { get; set; }
    
    [ForeignKey(nameof(ShoppingBag))]
    public Guid BagId { get; set; }
}