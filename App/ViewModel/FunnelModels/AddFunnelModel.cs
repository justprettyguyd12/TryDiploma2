using System.ComponentModel.DataAnnotations;
using Domain.Models.Crm;

namespace TryDiploma.ViewModel.FunnelModels;

public sealed class AddFunnelModel
{
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}