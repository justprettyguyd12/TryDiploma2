using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace TryDiploma.ViewModel;

public class UpdateBeatModel
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    public int PriceToBuy { get; set; }
    [Required]
    public int PriceToLease { get; set; }
    public int Bpm { get; set; }
}