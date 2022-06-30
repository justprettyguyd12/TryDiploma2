using System.Security.Claims;
using DataAccess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.Data.Entities;
using TryDiploma.ViewModel.AccountModels;

namespace TryDiploma.Controllers.Accounts;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IService<ShoppingBag> _bagManager;

    public AccountController(UserManager<ApplicationUser> userManager, IService<ShoppingBag> bagManager)
    {
        _userManager = userManager;
        _bagManager = bagManager;
    }

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ICollection<ApplicationUser> GetList()
    {
        return _userManager.Users.ToList();
    }

    /// <summary>
    /// Получить пользователя по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public ApplicationUser Get(Guid id)
    {
        return _userManager.FindByIdAsync(id.ToString()).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Создать пользователя. Не доделан
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<ActionResult<ApplicationUser>> Create(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<ActionResult<ApplicationUser>>(BadRequest("Шо то не то с моделькой"));
        
        var user = new ApplicationUser();
        user.UserName = model.UserName;
        
        //TODO chack later
        var bag = new ShoppingBag();
        bag.ClientId = user.Id;
        user.BagId = bag.Id;
        _bagManager.Create(bag);
        //
        
        var result = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
        if (!result.Succeeded) 
            return Task.FromResult<ActionResult<ApplicationUser>>(BadRequest("Не удалось зарегистрировать юзера"));
        
        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Client")).GetAwaiter().GetResult();
        return Task.FromResult<ActionResult<ApplicationUser>>(Ok($"Пользователь {model.UserName} создан"));
    }

    /// <summary>
    /// Обновить пользователя (не протестировано)
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult<ApplicationUser> Update(ApplicationUser user)
    {
        var result = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
        return (result.Succeeded)
            ? Ok("Пользователь обновлён")
            : BadRequest("Бита нема");
    }
    
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {
        var user = _userManager.FindByIdAsync(id.ToString()).GetAwaiter().GetResult();
        if (user is null)
            return BadRequest("Такого пользователя нет");
        
        var result = _userManager.DeleteAsync(user).GetAwaiter().GetResult();
        return !result.Succeeded
            ? BadRequest("Почему-то пользователь не удалился")
            : Ok($"Пользователь {user.UserName} удалён");
    }
}