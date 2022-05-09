namespace Domain.Models.Crm;

public sealed class Section
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FunnelId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Funnel Funnel { get; set; }
    
    public ICollection<Beat> Beats { get; set; }
}