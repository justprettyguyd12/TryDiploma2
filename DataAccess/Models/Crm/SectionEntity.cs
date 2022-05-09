using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.Crm;

[Table("Sections")]
internal sealed class SectionEntity : Entity
{
    [ForeignKey(nameof(Funnel))]
    public Guid FunnelId { get; set; }
    public FunnelEntity Funnel { get; set; }
    
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<BeatEntity> Beats { get; set; }
}