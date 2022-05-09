using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace TryDiploma.ViewModel;

public class AddBeatModel
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    [Required]
    public string PathToDemo { get; set; }
    [Required]
    public string PathToWav { get; set; }
    [Required]
    public string PathToTrackout { get; set; }

    [Required]
    public int PriceToBuy { get; set; }
    [Required]
    public int PriceToLease { get; set; }
    public int Bpm { get; set; }
    
    public BeatStatus Status { get; set; } = BeatStatus.InSale;
}