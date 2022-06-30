using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TryDiploma.Controllers.Accounts;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View(); 
    }
}