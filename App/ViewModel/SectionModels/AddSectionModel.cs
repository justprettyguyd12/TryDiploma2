using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace TryDiploma.ViewModel.SectionModels;

public class AddSectionModel
{
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }

    public ICollection<Beat> Beats { get; set; } = new List<Beat>();
}