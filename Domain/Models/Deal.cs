using System.Diagnostics.Contracts;

namespace Domain.Models;

public sealed class Deal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    public Guid BeatId { get; set; }
    public Guid ContractId { get; set; }

    public Client Client { get; set; }
    public Beat Beat { get; set; }
    public Contract Contract { get; set; }
    
    public DealType Type { get; set; }
    public int Price { get; set; }
}