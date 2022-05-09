using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("Contracts")]
internal sealed class ContractEntity : Entity
{
    [ForeignKey(nameof(Client))]
    public Guid ClientId { get; set; }
    public ClientEntity Client { get; set; }
    
    public string PathToFile { get; set; }
    
    public int TotalPrice { get; set; }
    
    public ICollection<DealEntity> Deals { get; set; }
}