using System.ComponentModel.DataAnnotations;

namespace TryDiploma.ViewModel.AccountModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; init; } = null!;

    [Required] 
    [Display(Name = "Password")]
    public string Password { get; init; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; init; } = null!;
}