using DataAccess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.Data.Entities;
using TryDiploma.ViewModel.BagModels;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class BagController : Controller
{
    private readonly IService<ShoppingBag> _bagService;
    private readonly IService<Beat> _beatService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BagController(IService<ShoppingBag> bagService, IService<Beat> beatService, UserManager<ApplicationUser> userManager)
    {
        _bagService = bagService;
        _beatService = beatService;
        _userManager = userManager;
    }

    /// <summary>
    /// Получить корзину пользователя (не протестировано)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    public ShoppingBag Get(Guid userId)
    {
        var bagId = _userManager.FindByIdAsync(userId.ToString())
            .GetAwaiter()
            .GetResult()
            .BagId;
        
        return _bagService.Get(userId)!;
    }

    /// <summary>
    /// Создать корзину для пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ShoppingBag> Create()
    {
        var bag = new ShoppingBag();
        _bagService.Create(bag);
        return bag;
    }

    /// <summary>
    /// Добавить бит в корзину
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult<ShoppingBag> AddBeat(AddBeatInBagModel model)
    {
        var beat = _beatService.Get(model.BeatId);
        if (beat is null)
            return BadRequest("Такого бита нет");
        
        var bag = _bagService.Get(model.BagId);
        if (bag is null)
            return BadRequest("Такой корзины нет");
        
        bag.Beats.Add(beat);
        _bagService.Update(bag);
        return Ok($"Бит {beat.Name} добавлен в корзину");
    }

    /// <summary>
    /// Удалить бит из корзины
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete]
    public ActionResult<ShoppingBag> DeleteBeat(AddBeatInBagModel model)
    {
        var beat = _beatService.Get(model.BeatId);
        if (beat is null)
            return BadRequest("Такого бита нет");
        
        var bag = _bagService.Get(model.BagId);
        if (bag is null)
            return BadRequest("Такой корзины нет");
        
        bag.Beats.Remove(beat);
        _bagService.Update(bag);
        return Ok($"Бит {beat.Name} удален из корзины");
    }
}