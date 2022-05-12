using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.Data.Entities;
using TryDiploma.ViewModel;
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
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found");
            return View(model);
        }

        var result = _signInManager.PasswordSignInAsync(user, model.Password, false, false)
            .GetAwaiter().GetResult();
        if (result.Succeeded)
            return Redirect(model.ReturnUrl);

        return View(model);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        var user = new ApplicationUser();
        user.UserName = model.UserName;

        var result = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
        if (!result.Succeeded) 
            return View(model);
        
        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Client")).GetAwaiter().GetResult();
        _signInManager.PasswordSignInAsync(user, model.Password, false, false)
            .GetAwaiter().GetResult();
        return Redirect("Index");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/Home/Index");
    }
    
}