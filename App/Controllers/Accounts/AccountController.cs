using System.Security.Claims;
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

    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
    public async Task<ActionResult<ApplicationUser>> Create(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Шо то не то с моделькой");
        
        var user = new ApplicationUser();
        user.UserName = model.UserName;
        //TODO создать и добавить корзину
        //BagId = 

        var result = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
        if (!result.Succeeded) 
            return BadRequest("Не удалось зарегистрировать юзера");
        
        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Client")).GetAwaiter().GetResult();
        return Ok($"Пользователь {model.UserName} создан");
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