using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.Crm;

[Table("Funnels")]
internal sealed class FunnelEntity : Entity
{
    [Required] 
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<SectionEntity> Sections { get; set; }
}