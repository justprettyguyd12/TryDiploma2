using System.Reflection.Metadata;

namespace Domain.Models;

public sealed class ShoppingBag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    
    public Client Client { get; set; }
    
    public ICollection<Beat> Beats { get; set; }
}