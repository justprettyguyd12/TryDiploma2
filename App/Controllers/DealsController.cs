using AutoMapper;
using DataAccess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.ViewModel.DealModels;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class DealsController : Controller
{
    private readonly IService<Deal> _dealService;
    private readonly IService<Client> _clientService;
    private IMapper _mapper;

    public DealsController(IService<Deal> dealService, IService<Client> clientService, IMapper mapper)
    {
        _dealService = dealService;
        _clientService = clientService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все сделки
    /// </summary>
    /// <returns>Список всех сделок</returns>
    [HttpGet]
    public ICollection<Deal> Get()
    {
        return _dealService.GetList();
    }

    /// <summary>
    /// Получить сделку по Id
    /// </summary>
    /// <param name="id">Guid сделки</param>
    /// <returns>Сделка или null, если сделка не найдена</returns>
    [HttpGet("{id:Guid}")]
    public Deal? Get(Guid id)
    {
        return _dealService.Get(id);
    }
    
    /// <summary>
    /// Получить сделки клиента
    /// </summary>
    /// <param name="clientId">Guid клиента</param>
    /// <returns>Список сделок клиента</returns>
    [HttpGet("forClient={clientId:Guid}")]
    public ICollection<Deal> GetForClient(Guid clientId)
    {
        return _clientService.Get(clientId)!
            .Deals
            .ToList();
    }

    /// <summary>
    /// Получить сделки по биту
    /// </summary>
    /// <param name="beatId">Guid бита</param>
    /// <returns>Коллекция всех сделок по биту</returns>
    [HttpGet("forBeat={beatId:Guid}")]
    public ICollection<Deal> GetForBeat(Guid beatId)
    {
        return _dealService.GetList()
            .Where(d => d.BeatId == beatId)
            .ToList();
    }
    
    /// <summary>
    /// Создать сделку. Возможно стоит разделить по типам сделок
    /// </summary>
    /// <param name="dealModel"></param>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpPost("client={clientId}")]
    public ActionResult<Deal> CreateDeal(AddDealModel dealModel, Guid clientId)
    {
        var deal = _mapper.Map<Deal>(dealModel);
        deal.ClientId = clientId;
        _dealService.Create(deal);
        return Ok($"Сделка {deal.Id} сформирована");
    }

    /// <summary>
    /// Изменение сделки, только для разработчика
    /// </summary>
    /// <param name="dealModel">Модель сделки</param>
    /// <param name="id">Guid сделки</param>
    /// <returns>Уведомит, если сделка обновлена успешно</returns>
    [HttpPut("id:Guid")]
    public ActionResult<Deal> Put(UpdateDealModel dealModel, Guid id)
    {
        var deal = _dealService.Get(id);
        if (deal is null)
            return BadRequest("Секции нема");
        UpdateDeal(dealModel, deal);
        _dealService.Update(deal);
        return Ok("Обновлена сделка: \n" + deal.Id);
    }

    /// <summary>
    /// Удалить сделку. Пока только для разработчика
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("id:Guid")]
    public ActionResult<Deal> Delete(Guid id)
    {
        _dealService.Delete(id);
        return Ok($"Сделка {id} удалена");
    }

    private void UpdateDeal(UpdateDealModel dealModel, Deal deal)
    {
        deal.BeatId = dealModel.BeatId;
        deal.ContractId = dealModel.ContractId;
        deal.ClientId = dealModel.ClientId;
        deal.Type = dealModel.Type;
        deal.Price = dealModel.Price;
    }
}