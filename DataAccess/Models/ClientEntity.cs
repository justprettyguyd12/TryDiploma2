using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("Clients")]
internal sealed class ClientEntity : Entity
{ 
    [ForeignKey(nameof(Bag))]
    public Guid BagId { get; set; }
    public ShoppingBagEntity Bag { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Patronymic { get; set; }
    
    [Required]
    public string Passport { get; set; }
    [Required]
    public string Email { get; set; }
    public string? Telegram { get; set; }
    
    public ICollection<DealEntity> Deals { get; set; }
}