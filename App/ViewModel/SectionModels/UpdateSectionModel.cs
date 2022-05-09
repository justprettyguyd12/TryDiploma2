using System.ComponentModel.DataAnnotations;

namespace TryDiploma.ViewModel.SectionModels;

public class UpdateSectionModel
{
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }
}