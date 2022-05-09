using AutoMapper;
using DataAccess.Services;
using Domain.Models.Crm;
using Microsoft.AspNetCore.Mvc;
using TryDiploma.ViewModel.FunnelModels;

namespace TryDiploma.Controllers;

[ApiController]
[Route("[controller]")]
public class FunnelsController : Controller
{
    private readonly IService<Funnel> _service;
    private readonly IMapper _mapper;

    public FunnelsController(IService<Funnel> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить список всех воронок
    /// </summary>
    /// <returns>
    /// Коллекция всех воронок
    /// </returns>
    [HttpGet]
    public ICollection<Funnel> Get()
    {
        return _service.GetList();
    }

    /// <summary>
    /// Получить воронку по Id
    /// </summary>
    /// <param name="id">Guid воронки</param>
    /// <returns>
    /// Воронка с соответствующим Id, или null, если такой нет
    /// </returns>
    [HttpGet("{id:Guid}")]
    public Funnel? Get(Guid id)
    {
        return _service.Get(id);
    }

    /// <summary>
    /// Добавить воронку
    /// </summary>
    /// <param name="funnelModel">
    /// ViewModel для воронки, обязательно содержащий имя и описание.
    /// Пустой список секций создаётся автоматически
    /// </param>
    /// <returns>
    /// При успешном выполнении возвращает имя добавленной воронки
    /// </returns>
    [HttpPost]
    public ActionResult<Funnel> AddFunnel(AddFunnelModel funnelModel)
    {
        var funnel = _mapper.Map<Funnel>(funnelModel);
        _service.Create(funnel);
        return Ok("Добавлена воронка: \n" + funnel.Name);
    }
    
    /// <summary>
    /// Обновление воронки. Обновляет имя или описание.
    /// </summary>
    /// <param name="funnel">Полная доменная модель воронки, включая все секции</param>
    /// <returns>
    /// При успешном выполнении возвращает имя воронки
    /// </returns>
    [HttpPut]
    public ActionResult<Funnel> Put(Funnel funnel)
    {
        _service.Update(funnel);
        return Ok("Обновлена воронка: \n" + funnel.Name);
    }

    /// <summary>
    /// Удалить воронку по Id
    /// </summary>
    /// <param name="id">
    /// Guid удаляемой воронки
    /// </param>
    /// <returns>
    /// Уведомит об успешном возврате
    /// </returns>
    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {
        _service.Delete(id);
        return Ok("Воронка удалена");
    }
}