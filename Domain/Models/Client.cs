namespace Domain.Models;

public sealed class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BagId { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    
    public string Passport { get; set; }
    public string Email { get; set; }
    public string? Telegram { get; set; }
    
    public ShoppingBag Bag { get; set; }
    
    public ICollection<Deal> Deals { get; set; }
}