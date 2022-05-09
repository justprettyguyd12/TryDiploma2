namespace Domain.Models.Crm;

public sealed class Funnel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Section> Sections { get; set; }
}