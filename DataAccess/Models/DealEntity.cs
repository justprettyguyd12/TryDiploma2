using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace DataAccess.Models;

[Table("Deals")]
internal sealed class DealEntity : Entity
{
    [ForeignKey(nameof(Client))]
    public Guid ClientId { get; set; }
    public ClientEntity Client { get; set; }
    
    [ForeignKey(nameof(Beat))]
    public Guid BeatId { get; set; }
    public BeatEntity Beat { get; set; }
    
    [ForeignKey(nameof(Contract))]
    public Guid ContractId { get; set; }
    public ContractEntity Contract { get; set; }
    
    [Column(TypeName = "text")]
    public DealType Type { get; set; }
    public int Price { get; set; }
    
}