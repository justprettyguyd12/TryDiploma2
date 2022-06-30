using System.ComponentModel.DataAnnotations;

namespace TryDiploma.ViewModel.AccountModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; init; } = null!;

    [Required]
    [Display(Name = "Password")]
    public string Password { get; init; } = null!;
}