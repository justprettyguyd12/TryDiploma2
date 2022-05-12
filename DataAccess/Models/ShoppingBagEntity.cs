using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("Bags")]
internal sealed class ShoppingBagEntity : Entity
{
    [ForeignKey(nameof(Client))]
    public Guid? ClientId { get; set; }
    public ClientEntity? Client { get; set; }
    
    public ICollection<BeatEntity> Beats { get; set; }
}