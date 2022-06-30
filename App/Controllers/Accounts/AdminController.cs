using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.Data.Entities;
using TryDiploma.ViewModel.AccountModels;

namespace TryDiploma.Controllers.Accounts;

//TODO задокументировать методы 
[Route("[controller]/[action]")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public bool? IsAuthenticated()
    {
        return @User.Identity?.IsAuthenticated;
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Policy = "Administrator")]
    public IActionResult Administrator()
    {
        return View();
    }
    
    [HttpGet]
    [Authorize(Policy = "Client")]
    public IActionResult Client()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        return Ok();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]LoginViewModel model)
    {
        if (!ModelState.IsValid) 
            return BadRequest("Форма заполнена некорректно");

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found");
            return BadRequest("Пользователь не найден");
        }

        var result = _signInManager.PasswordSignInAsync(user, model.Password, false, false)
            .GetAwaiter().GetResult();
        if (result.Succeeded)
            return Ok();

        return BadRequest("Пароль неверный");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public RegisterViewModel Register()
    {
        return new RegisterViewModel();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register([FromBody]RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetValidationState(""));

        var user = new ApplicationUser();
        user.UserName = model.UserName;

        var result = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
        if (!result.Succeeded) 
            return View(model);
        
        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Client")).GetAwaiter().GetResult();
        _signInManager.PasswordSignInAsync(user, model.Password, false, false)
            .GetAwaiter().GetResult();
        return Redirect("/Home/Index");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/Home/Index");
    }
    
}