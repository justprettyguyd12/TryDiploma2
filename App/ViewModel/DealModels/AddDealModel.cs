using Domain.Models;

namespace TryDiploma.ViewModel.DealModels;

/// <summary>
/// Только для разработчика
/// </summary>
public class AddDealModel
{
    public Guid BeatId { get; set; }
    public Guid ContractId { get; set; }

    public DealType Type { get; set; }
    public int Price { get; set; }
}