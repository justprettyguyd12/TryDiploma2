using Domain.Models;

namespace TryDiploma.ViewModel.DealModels;

public class UpdateDealModel
{
    public Guid ClientId { get; set; }
    public Guid BeatId { get; set; }
    public Guid ContractId { get; set; }
    
    public DealType Type { get; set; }
    public int Price { get; set; }
}