namespace Domain.Models;

public sealed class Contract
{
    public Guid Id { get; set; } = new Guid();
    public Guid ClientId { get; set; }
    
    public Client Client { get; set; }
    public string PathToFile { get; set; }
    public int TotalPrice { get; set; }
    
    public ICollection<Deal> Deals { get; set; }
}