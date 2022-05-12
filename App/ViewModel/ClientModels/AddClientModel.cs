using System.ComponentModel.DataAnnotations;

namespace TryDiploma.ViewModel.ClientModels;

public class AddClientModel
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }
    
    [Required]
    [MinLength(2)]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(2)]
    public string Patronymic { get; set; }
    
    [Required]
    [MinLength(10)]
    [MaxLength(10)]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Некорректные паспортные данные")]
    public string Passport { get; set; }
    [Required]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public string Email { get; set; }
    public string? Telegram { get; set; }
}