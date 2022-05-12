using AutoMapper;
using DataAccess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.ViewModel.ClientModels;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController : Controller
{
    private readonly IService<Client> _clientService;
    private readonly IService<Deal> _dealService;
    private readonly IMapper _mapper;

    public ClientsController(IService<Client> clientService, IService<Deal> dealService, IMapper mapper)
    {
        _clientService = clientService;
        _dealService = dealService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить список всех клиентов (не протестировано)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ICollection<Client> GetList()
    {
        return _clientService.GetList();
    }

    /// <summary>
    /// Получить клиента по Id (не протестировано)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public Client? Get(Guid id)
    {
        return _clientService.Get(id);
    }

    /// <summary>
    /// Получить всех клиентов по биту (скорее всего не пригодится) (не протестировано)
    /// </summary>
    /// <param name="beatId"></param>
    /// <returns></returns>
    [HttpGet("forBeat={beatId:Guid}")]
    public ICollection<Client> GetForBeat(Guid beatId)
    {
        return _dealService.GetList()
            .Where(d => d.BeatId == beatId)
            .Select(d => d.Client)
            .ToList();
    }

    /// <summary>
    /// Добавить клиента (не протестировано)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<Client> Add(AddClientModel model)
    {
        var client = _mapper.Map<Client>(model);
        if (client is null)
            return BadRequest();
        
        _clientService.Create(client);
        //TODO здесь должно быть добавление корзины (от юзера например)
        return Ok($"Клиент {model.LastName} {model.FirstName} добавлен");
    }

    /// <summary>
    /// Обновить клиента (не протестировано)
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult<Client> Update(Client client)
    {
        _clientService.Update(client);
        return Ok($"Обновлен клиент {client.LastName} {client.FirstName}");
    }

    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {
        _clientService.Delete(id);
        return Ok($"Клиент {id} удален");
    }
}