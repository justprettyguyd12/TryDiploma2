using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TryDiploma.Controllers.Accounts;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var userClaims = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role);
        ViewBag.Role = userClaims?.Value!;
        return View();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View(); 
    }
}