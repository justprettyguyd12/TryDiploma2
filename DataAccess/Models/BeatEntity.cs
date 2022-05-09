using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Crm;
using Domain.Models;

namespace DataAccess.Models;

[Table("Beats")]
internal sealed class BeatEntity : Entity
{
    [ForeignKey(nameof(Section))]
    public Guid SectionId { get; set; }
    public SectionEntity Section { get; set; }
    
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string PathToDemo { get; set; }
    public string PathToWav { get; set; }
    public string PathToTrackout { get; set; }

    [Required]
    public int PriceToBuy { get; set; }
    [Required]
    public int PriceToLease { get; set; }
    [Required]
    public int Bpm { get; set; }
    
    [Column(TypeName = "text")]
    public BeatStatus Status { get; set; }
}