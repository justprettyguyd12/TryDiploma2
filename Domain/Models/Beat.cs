using Domain.Models.Crm;

namespace Domain.Models;

public sealed class Beat
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SectionId { get; set; }
        
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string PathToDemo { get; set; }
    public string PathToWav { get; set; }
    public string PathToTrackout { get; set; }

    public int PriceToBuy { get; set; }
    public int PriceToLease { get; set; }
    public int Bpm { get; set; }
    public BeatStatus Status { get; set; }
    
    public Section Section { get; set; }
}